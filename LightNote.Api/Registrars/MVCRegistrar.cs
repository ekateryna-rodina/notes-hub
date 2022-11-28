﻿using System;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace LightNote.Api.Registrars
{
	public class MVCRegistrar : IWebAppBuilderRegistrar
	{
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddApiVersioning(config => {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            builder.Services.AddVersionedApiExplorer(config => {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddSwaggerGen();
        }
    }
}

