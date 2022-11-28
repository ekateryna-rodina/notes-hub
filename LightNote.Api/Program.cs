using LightNote.Api.Extensions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));

var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));


app.Run();

