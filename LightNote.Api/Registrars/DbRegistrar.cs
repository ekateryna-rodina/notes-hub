using System.Net;
using LightNote.Api.Options;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Dal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Api.Registrars
{
    public class DbRegistrar : IWebAppBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = GetConnectionString(builder);
            builder.Services.AddDbContext<AppDbContext>(options => options
           .UseSqlServer(connectionString)
           .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));

            builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private string GetConnectionString(WebApplicationBuilder builder)
        {
            if (builder.Environment.EnvironmentName == "Staging")
            {
                var dockerHostMachineIpAddress = Dns.GetHostAddresses(new Uri("http://host.docker.internal").Host)[0].ToString();
                var config = builder.Configuration;
                var user = config["DbUser"];
                var password = config["DbPassword"];
                var port = config["DbPort"];
                var name = config["DbName"];

                return $"Server={dockerHostMachineIpAddress}, {port};Initial Catalog={name};User ID={user};Password={password};TrustServerCertificate=True;";
            } else if (builder.Environment.EnvironmentName == "IntegrationTesting") {
                return builder.Configuration.GetConnectionString("Integration")!;
            }

            return builder.Configuration.GetConnectionString("Default")!;
        }
    }
}

