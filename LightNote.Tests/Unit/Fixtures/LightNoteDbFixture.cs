using System;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Tests.Utils;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;

namespace LightNote.Tests.Unit.Fixtures
{
	public class LightNoteDbFixture : DbFixture
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static IServiceCollection _services;
     
        private static NpgsqlConnection _dbConnection;
        private static Respawner _respawner;
        private string _dockerContainerId;
        private string _dockerPort;
        private string _dockerIp;
        private static string _dockerConnectionString;
        public LightNoteDbFixture()
		{
		}

        public override async Task DisposeAsync()
        {
            await ResetState();
        }

        public override async Task InitializeAsync()
        {
            await RunBeforeAnyTests();
        }

        private async Task RunBeforeAnyTests()
        {
            (_dockerContainerId, _dockerPort, _dockerIp) = await DockerPostgresqlDatabaseUtilities.EnsureDockerIsStartedAndGetContainerIdAndPortAsync();
            _dockerConnectionString = DockerPostgresqlDatabaseUtilities.GetPostgresConnectionString(_dockerPort, _dockerIp);


            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                  .Build();
            _services = ConfigureServices();
            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();
            _dbConnection = new NpgsqlConnection(_dockerConnectionString);
            EnsureDatabase();
            Task.Run(async () =>
            {
                _respawner = await InitRespawn();
            }).Wait();
        }
        private IServiceCollection ConfigureServices() {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetAllUsers));
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_dockerConnectionString));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
        private async Task<Respawner> InitRespawn()
        {
            try
            {
                //using var dbConnection = new NpgsqlConnection(_dockerConnectionString);
                await _dbConnection.OpenAsync();
                var respawner = await Respawner.CreateAsync(_dbConnection,
                    new RespawnerOptions
                    {
                        SchemasToInclude = new[] { "public" },
                        DbAdapter = DbAdapter.Postgres
                    });

                return respawner;
            }
            catch (Exception ex)
            {
                await _dbConnection.CloseAsync();
                return null;
            }
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                
            }
        }

        public static async Task ResetState()
        {
            try
            {
                //using var dbConnection = new NpgsqlConnection(_dockerConnectionString);
                //await _dbConnection.OpenAsync();
                await _respawner.ResetAsync(_dbConnection);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
            finally {
                await _dbConnection.CloseAsync();
            }
        }

        public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();


            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) where TResponse : class
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                return await mediator.Send(request);
            }
        }
    }
}

