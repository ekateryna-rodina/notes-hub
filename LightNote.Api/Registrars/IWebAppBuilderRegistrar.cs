using System;
namespace LightNote.Api.Registrars
{
    public interface IWebAppBuilderRegistrar : IRegistrar
    {
        void RegisterServices(WebApplicationBuilder builde);
    }
}

