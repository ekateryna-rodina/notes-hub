using LightNote.Application.BusinessLogic.UserProfiles.Queries;
using LightNote.Application.Options;
using LightNote.Application.Services.TokenGenerators;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Dal.Services;
using LightNote.IntegrationTests.Utils;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using LightNote.Api;
using LightNote.Api.Contracts.Identity.Request;
using LightNote.Api.Contracts.Identity.Response;
using LightNote.Api.Utils;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;

namespace LightNote.IntegrationTests.Setup
{
    public class TestUtils
    {
        public static HttpClient HttpClient = default!;
        private static IConfiguration _configuration = default!;
        private static SqlConnection _dbConnection = default!;
        private static Respawner _respawner = default!;
        private static string _testConnectionString = default!;

        public static async Task RunBeforeAnyTests()
        {
            InitConfiguration();
            var appFactory = SetupWebHostClient();
            await InitRespawn();
        }

        private static void InitConfiguration()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
              .Build();
            _testConnectionString = _configuration.GetConnectionString("Integration")!;
        }

        private static WebApplicationFactory<Program> SetupWebHostClient()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("IntegrationTesting");
                });
            HttpClient = appFactory.CreateClient();
            return appFactory;
        }
        public static async Task RunAfterAllTests()
        {
            await _dbConnection.CloseAsync();
            using (var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                 .UseSqlServer(_testConnectionString)
                 .Options))
            {
                context.Database.EnsureDeleted();
            }
        }
        private static async Task InitRespawn()
        {
            try
            {
                _dbConnection = new SqlConnection(_testConnectionString);
                await _dbConnection.OpenAsync();
                _respawner = await Respawner.CreateAsync(_dbConnection,
                    new RespawnerOptions
                    {
                        SchemasToInclude = new[] { "dbo" },
                        DbAdapter = DbAdapter.SqlServer
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}

