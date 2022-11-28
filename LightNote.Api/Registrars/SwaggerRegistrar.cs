using System;

namespace LightNote.Api.Registrars
{
	public class SwaggerRegistrar : IWebAppRegistrar
    {
        public void RegisterPipelineComponensts(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}

