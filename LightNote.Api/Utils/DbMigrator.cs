using System;
using LightNote.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LightNote.Api.Utils
{
    public static class DbMigrator
    {
        public static void Run(WebApplication builder)
        {
            try
            {
                using (var scope = builder.Services.CreateScope())
                {
                    var databaseCreator = scope.ServiceProvider.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                    var context = scope.ServiceProvider.GetService<AppDbContext>();
                    if (databaseCreator != null)
                    {
                        if (!databaseCreator.CanConnect())
                        {
                            databaseCreator.Create();
                        }
                    }
                    if (context != null)
                    {
                        context.Database.Migrate();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

