using System;

namespace LightNote.Api.Registrars
{
	public class DbRegistrar  : IWebAppBuilderRegistrar
	{
		public DbRegistrar()
		{
		}

        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetSection("Database:DefaultConnection");
        }
    }
}

