using System.Reflection;
using LightNote.Application.BusinessLogic.Identity.CommandHandlers;
using LightNote.Application.BusinessLogic.Users.Queries;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightNote.Tests.Unit
{
    [CollectionDefinition("SetupFixture")]
    public class SetUpFixture : ICollectionFixture<ConfiguredServices>
    {
        private static IServiceScopeFactory _scopeFactory;
        
        public SetUpFixture(ConfiguredServices services) {
            _scopeFactory = services.ScopeFactory;
        }
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request) where TResponse : class
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            return await mediator.Send(request);
        }
    }

    public class ConfiguredServices
    {
        private readonly IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        public IServiceScopeFactory ScopeFactory { get { return _scopeFactory; } }
        public ConfiguredServices()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                  .Build();
            var services = new ServiceCollection();
            services.AddMediatR(typeof(GetAllUsers));
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(_configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
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

