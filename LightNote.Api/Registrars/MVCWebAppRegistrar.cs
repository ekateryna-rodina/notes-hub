using System;

namespace LightNote.Api.Registrars
{
    public class MVCWebAppRegistrar : IWebAppRegistrar
    {
        public void RegisterPipelineComponensts(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}

