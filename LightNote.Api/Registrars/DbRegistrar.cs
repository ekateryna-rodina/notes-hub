using LightNote.Dal;
using LightNote.Dal.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Api.Registrars
{
    public class DbRegistrar  : IWebAppBuilderRegistrar
	{
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(builder.Configuration
            .GetValue<string>("ConnectionStrings:DefaultConnection"))
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));

            builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

