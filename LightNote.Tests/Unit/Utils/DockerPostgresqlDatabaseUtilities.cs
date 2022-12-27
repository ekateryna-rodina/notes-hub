using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LightNote.Tests.Utils
{
    public static class DockerPostgresqlDatabaseUtilities
    {
        private static IConfiguration _configuration;
        public static string DB_PASSWORD { get { return _configuration["Database:Password"]; } }
		public const string DB_IMAGE = "postgres";
		public const string DB_IMAGE_TAG = "latest";
        public static string DB_USER { get { return _configuration["Database:User"]; } }
        public static string DB_NAME { get { return _configuration["Database:Name"]; } }
        public const string CONTAINER_NAME = "LightNoteUnitTestsPostgres";
        public const string VOLUME_NAME = "LightNoteUnitTestsPostgresVolume";

        static DockerPostgresqlDatabaseUtilities() {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                 .Build();
        }

        public static async Task<(string containerId, string port, string ip)> EnsureDockerIsStartedAndGetContainerIdAndPortAsync() {
            await CleanupRunningContainers();
            await CleanupRunningVolumes();
            var dockerClient = GetDockerClient();
            var freePort = GetFreePort();
            
            // This call ensures that the latest SQL Server Docker image is pulled
            await dockerClient.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = $"{DB_IMAGE}:{DB_IMAGE_TAG}"
            }, null, new Progress<JSONMessage>());

            // create a volume, if one doesn't already exist
            var volumeList = await dockerClient.Volumes.ListAsync();
            var volumeCount = volumeList.Volumes.Where(v => v.Name == VOLUME_NAME).Count();
            if (volumeCount <= 0)
            {
                await dockerClient.Volumes.CreateAsync(new VolumesCreateParameters
                {
                    Name = VOLUME_NAME,
                });
            }

            // create container, if one doesn't already exist
            var contList = await dockerClient
                .Containers.ListContainersAsync(new ContainersListParameters() { All = true });
            var existingCont = contList
                .Where(c => c.Names.Any(n => n.Contains(CONTAINER_NAME))).FirstOrDefault();

            if (existingCont == null)
            {
                try
                {
                    var container = await dockerClient
                    .Containers
                    .CreateContainerAsync(new CreateContainerParameters
                    {
                        Name = CONTAINER_NAME,
                        Image = $"{DB_IMAGE}:{DB_IMAGE_TAG}",
                        Env = new List<string>
                        {
                            $"POSTGRES_PASSWORD={DB_PASSWORD}"
                        },
                        HostConfig = new HostConfig
                        {
                            PortBindings = new Dictionary<string, IList<PortBinding>>
                            {
                                { "5432/tcp", new List<PortBinding> { new PortBinding { HostPort = "5432" } } }
                            },
                            //Binds = new List<string>
                            //{
                            //    $"{VOLUME_NAME}:/LightNote_data"
                            //}
                        },
                        ExposedPorts = new Dictionary<string, EmptyStruct>
                            {
                                {"5432/tcp", new EmptyStruct() }
                            },
                    });

                    await dockerClient
                        .Containers
                        .StartContainerAsync(container.ID, new ContainerStartParameters());

                    //await WaitUntilDatabaseAvailableAsync(freePort); 
                    var containerInfo = await dockerClient.Containers.InspectContainerAsync(container.ID);
                    await CreateDatabase(freePort, containerInfo.NetworkSettings.Networks["bridge"].IPAddress);
                    return (container.ID, "5432", containerInfo.NetworkSettings.Networks["bridge"].IPAddress);
                }
                catch (Exception ex)
                {
                    return (string.Empty, string.Empty, string.Empty);
                }
            }
            
            return (existingCont.ID, "5432", existingCont.NetworkSettings.Networks["bridge"].IPAddress);
        }

        private static async Task CreateDatabase(string databasePort, string ip) {
            try
            {
                var connectionString = GetPostgresConnectionString("5432", ip, "postgres");
                using var postgresConnection = new NpgsqlConnection(connectionString);
                await postgresConnection.OpenAsync();
                //postgresConnection.ChangeDatabase("postgres");
                using var command = new NpgsqlCommand($"CREATE DATABASE {DB_NAME}", postgresConnection);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private static async Task WaitUntilDatabaseAvailableAsync(string databasePort, string ip)
        {
            var start = DateTime.UtcNow;
            const int maxWaitTimeSeconds = 60;
            var connectionEstablished = false;
            while (!connectionEstablished && start.AddSeconds(maxWaitTimeSeconds) > DateTime.UtcNow)
            {
                try
                {
                    var connectionString = GetPostgresConnectionString(databasePort, ip);
                    using var postgresConnection = new NpgsqlConnection(connectionString);
                    await postgresConnection.OpenAsync();
                    connectionEstablished = true;
                }
                catch(Exception ex)
                {
                    // If opening the Postgres connection fails, Postgres Server is not ready yet
                    await Task.Delay(500);
                }
            }

            if (!connectionEstablished)
            {
                throw new Exception($"Connection to the Postgres docker database could not be established within {maxWaitTimeSeconds} seconds.");
            }

            return;
        }
        public static string GetPostgresConnectionString(string port, string ip, string? name = "") {
            var dbName = string.IsNullOrEmpty(name) ? DB_NAME : name;
            return $"Server=localhost,{port};Database={dbName};Uid={DB_USER};Pwd={DB_PASSWORD};";
        }

        private static DockerClient GetDockerClient()
        {
            var dockerUri = IsRunningOnWindows()
                ? "npipe://./pipe/docker_engine"
                : "unix:///var/run/docker.sock";
            return new DockerClientConfiguration(new Uri(dockerUri))
                .CreateClient();
        }

        private static bool IsRunningOnWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        private static string GetFreePort()
        {
            // From https://stackoverflow.com/a/150974/4190785
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port.ToString();
        }

        private static async Task CleanupRunningContainers(int hoursTillExpiration = -24)
        {
            var dockerClient = GetDockerClient();

            var runningContainers = await dockerClient.Containers
                .ListContainersAsync(new ContainersListParameters());

            foreach (var runningContainer in runningContainers.Where(cont => cont.Names.Any(n => n.Contains(CONTAINER_NAME))))
            {
                // Stopping all test containers that are older than 24 hours
                var expiration = hoursTillExpiration > 0
                    ? hoursTillExpiration * -1
                    : hoursTillExpiration;
                if (runningContainer.Created < DateTime.UtcNow.AddHours(expiration))
                {
                    try
                    {
                        await EnsureDockerContainersStoppedAndRemovedAsync(runningContainer.ID);
                    }
                    catch
                    {
                        // Ignoring failures to stop running containers
                    }
                }
            }
        }

        private static async Task CleanupRunningVolumes(int hoursTillExpiration = -24)
        {
            var dockerClient = GetDockerClient();

            var runningVolumes = await dockerClient.Volumes.ListAsync();

            foreach (var runningVolume in runningVolumes.Volumes.Where(v => v.Name == VOLUME_NAME))
            {
                // Stopping all test volumes that are older than 24 hours
                var expiration = hoursTillExpiration > 0
                    ? hoursTillExpiration * -1
                    : hoursTillExpiration;
                if (DateTime.Parse(runningVolume.CreatedAt) < DateTime.UtcNow.AddHours(expiration))
                {
                    try
                    {
                        await EnsureDockerVolumesRemovedAsync(runningVolume.Name);
                    }
                    catch
                    {
                        // Ignoring failures to stop running containers
                    }
                }
            }
        }

        private static async Task EnsureDockerContainersStoppedAndRemovedAsync(string dockerContainerId)
        {
            var dockerClient = GetDockerClient();
            await dockerClient.Containers
                .StopContainerAsync(dockerContainerId, new ContainerStopParameters());
            await dockerClient.Containers
                .RemoveContainerAsync(dockerContainerId, new ContainerRemoveParameters());
        }

        private static async Task EnsureDockerVolumesRemovedAsync(string volumeName)
        {
            var dockerClient = GetDockerClient();
            await dockerClient.Volumes.RemoveAsync(volumeName);
        }
    }
}

