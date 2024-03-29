﻿using System.Net;
using System.Net.Sockets;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LightNote.IntegrationTests.Utils
{
    public static class DockerMssqlDatabaseUtilities
    {
        public static string DB_PASSWORD = "Qwerty12345!";
        public const string DB_IMAGE = "mcr.microsoft.com/mssql/server";
        public const string DB_IMAGE_TAG = "latest";
        public static string DB_USER = "sa";
        public static string DB_NAME = "notehubtestdb";
        public const string CONTAINER_NAME = "LightNoteIntegrationsSQL";
        public const string VOLUME_NAME = "LightNoteUnitTestsSQLVolume";

        public static async Task CleanupContainerAndVolumes()
        {
            await CleanupRunningContainers();
            await CleanupRunningVolumes();
        }
        public static async Task<(string containerId, string port, string ip)> EnsureDockerIsStartedAndGetContainerIdAndPortAsync()
        {
            await CleanupContainerAndVolumes();
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
                            "ACCEPT_EULA=Y",
                            $"SA_PASSWORD={DB_PASSWORD}"
                        },
                        HostConfig = new HostConfig
                        {
                            PortBindings = new Dictionary<string, IList<PortBinding>>
                            {
                                { "1433/tcp", new List<PortBinding> { new PortBinding { HostPort = "1433" } } }
                            },
                        },
                        ExposedPorts = new Dictionary<string, EmptyStruct>
                            {
                                {"1433/tcp", new EmptyStruct() }
                            },
                    });

                    await dockerClient
                        .Containers
                        .StartContainerAsync(container.ID, new ContainerStartParameters());

                    var containerInfo = await dockerClient.Containers.InspectContainerAsync(container.ID);
                    await CreateDatabase(freePort, containerInfo.NetworkSettings.Networks["bridge"].IPAddress);
                    return (container.ID, "1433", containerInfo.NetworkSettings.Networks["bridge"].IPAddress);
                }
                catch (Exception ex)
                {
                    return (string.Empty, string.Empty, string.Empty);
                }
            }

            return (existingCont.ID, "1433", existingCont.NetworkSettings.Networks["bridge"].IPAddress);
        }

        private static async Task CreateDatabase(string databasePort, string ip)
        {
            try
            {
                var connectionString = GetTestConnectionString("1433");
                using var postgresConnection = new SqlConnection(connectionString);
                await postgresConnection.OpenAsync();
                using var command = new SqlCommand($"CREATE DATABASE {DB_NAME}", postgresConnection);
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public static string GetTestConnectionString(string port)
        {
            return $"Server=localhost,{port};Initial Catalog={DB_NAME};User ID={DB_USER};Password={DB_PASSWORD};TrustServerCertificate=True;";
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

        private static async Task CleanupRunningContainers()
        {
            var dockerClient = GetDockerClient();

            var runningContainers = await dockerClient.Containers
                .ListContainersAsync(new ContainersListParameters());

            foreach (var runningContainer in runningContainers.Where(cont => cont.Names.Any(n => n.Contains(CONTAINER_NAME))))
            {
                try
                {
                    await EnsureDockerContainersStoppedAndRemovedAsync(runningContainer.ID);
                }
                catch (Exception ex)
                {
                    await Task.FromException(ex);
                }
            }
        }

        private static async Task CleanupRunningVolumes()
        {
            var dockerClient = GetDockerClient();

            var runningVolumes = await dockerClient.Volumes.ListAsync();

            foreach (var runningVolume in runningVolumes.Volumes.Where(v => v.Name == VOLUME_NAME))
            {
                try
                {
                    await EnsureDockerVolumesRemovedAsync(runningVolume.Name);
                }
                catch (Exception ex)
                {
                    await Task.FromException(ex);
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

