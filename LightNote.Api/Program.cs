using LightNote.Api.Extensions;
using LightNote.Api.Utils;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices(typeof(Program), builder.Configuration);
var app = builder.Build();
app.RegisterPipelineComponents(typeof(Program));
DbMigrator.Run(app);
app.Run();



