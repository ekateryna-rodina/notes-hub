using System;
using System.Reflection.Emit;
using LightNote.Dal;
using Microsoft.EntityFrameworkCore;

namespace LightNote.Api.Registrars
{
	public class DbRegistrar  : IWebAppBuilderRegistrar
	{
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(builder.Configuration
            .GetValue<string>("Database:DefaultConnection"))
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s)));
        }
    }
}

