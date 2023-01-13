using System.Reflection;
using MediatR;
using LightNote.Application.BusinessLogic.Identity.CommandHandlers;

namespace LightNote.Api.Registrars
{
	public class MediatrRegistrar : IWebAppBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(RegisterIdentityHandler).GetTypeInfo().Assembly);
        }
    }
}

