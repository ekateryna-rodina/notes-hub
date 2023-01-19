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
                    var context = scope.ServiceProvider.GetService<AppDbContext>();
                    if (context != null)
                    {
                        if (!context.Database.CanConnect())
                        {
                            context.Database.EnsureCreated();
                        }
                        else {
                            context.Database.Migrate();
                        }
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

