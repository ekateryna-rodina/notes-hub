using System;
namespace LightNote.Api.Registrars
{
	public interface IWebAppRegistrar : IRegistrar
    {
		void RegisterPipelineComponensts(WebApplication app);
	}
}

