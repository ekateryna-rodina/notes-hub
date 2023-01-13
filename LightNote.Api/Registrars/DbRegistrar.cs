using System.Net;
using LightNote.Api.Options;
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
            var cs = string.Empty;
            if (builder.Environment.IsDevelopment())
            {
                cs = "Server=localhost, 1433;Initial Catalog=master;User ID=sa;Password=Qwerty12345!;TrustServerCertificate=True;";
            }
            else
            {
                var config = builder.Configuration;
                var user = config["DbUser"];
                var password = config["DbPassword"];
                var port = config["DbPort"];
                var name = config["DbName"];

                cs = $"Server={DockerHostMachineIpAddress}, {port};Initial Catalog={name};User ID={user};Password={password};TrustServerCertificate=True;";
            }

            builder.Services.AddDbContext<AppDbContext>(options => options
            .UseSqlServer(cs)
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));

            builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

