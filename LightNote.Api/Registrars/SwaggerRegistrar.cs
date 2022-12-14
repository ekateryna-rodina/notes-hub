using System;
using LightNote.Api.Options;

namespace LightNote.Api.Registrars
{
	public class SwaggerRegistrar : IWebAppBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<SwaggerOptions>();
        }
    }
}

