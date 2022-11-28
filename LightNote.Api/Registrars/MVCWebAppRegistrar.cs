using System;

namespace LightNote.Api.Registrars
{
	public class MVCWebAppRegistrar : IWebAppRegistrar
	{
        public void RegisterPipelineComponensts(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}

