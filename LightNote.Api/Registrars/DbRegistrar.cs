using System.Net;
using LightNote.Dal;
using LightNote.Dal.Contracts;
using LightNote.Dal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Api.Registrars
{
    public class DbRegistrar : IWebAppBuilderRegistrar
    {
        public static string DockerHostMachineIpAddress => Dns.GetHostAddresses(new Uri("http://host.docker.internal").Host)[0].ToString();
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var config = builder.Configuration;
            var server = config["DbServer"];
            var user = config["DbUser"];
            var password = config["DbPassword"];
            var port = config["DbPort"];
            var name = config["DbName"];

            var cs = $"Server={DockerHostMachineIpAddress}, {port};Initial Catalog={name};User ID={user};Password={password};TrustServerCertificate=True;";
            builder.Services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(cs)
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));

            builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

