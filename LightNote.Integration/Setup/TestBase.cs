using System;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Application.Contracts;
using LightNote.Application.Options;
using LightNote.Application.Services;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.IntegrationTests.Utils;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;

namespace LightNote.IntegrationTests.Setup
{
    public class TestBase
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static IServiceCollection _services;

        private static NpgsqlConnection _dbConnection;
        private static Respawner _respawner;
        private static string _dockerContainerId;
        private static string _dockerPort;
        private static string _dockerIp;
        private static string _dockerConnectionString;

        public static IUnitOfWork UnitOfWork
        {
            get
            {
                var scope = _scopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                return unitOfWork;
            }
        }

        public static async Task RunBeforeAnyTests()
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
        public static async Task RunAfterAllTests()
        {
            await _dbConnection.CloseAsync();
            await DockerPostgresqlDatabaseUtilities.CleanupContainerAndVolumes();
        }
        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetAllUsers));
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_dockerConnectionString));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));
            services.AddTransient<IToken, JwtService>();
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
        private static async Task<Respawner> InitRespawn()
        {
            try
            {
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
                await _respawner.ResetAsync(_dbConnection);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
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

