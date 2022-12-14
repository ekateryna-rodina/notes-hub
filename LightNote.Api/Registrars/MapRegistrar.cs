using AutoMapper;
using System.Reflection;
using AutoMapper.Configuration;
using MediatR;
using LightNote.Application.BusinessLogic.Identity.CommandHandlers;

namespace LightNote.Api.Registrars
{
	public class MapRegistrar : IWebAppBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(config =>
            {
                var profileTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t));

                foreach (var type in profileTypes)
                {
                    config.AddProfile(type);
                }
            });

            builder.Services.AddMediatR(typeof(RegisterIdentityHandler).GetTypeInfo().Assembly);
        }
    }
}

