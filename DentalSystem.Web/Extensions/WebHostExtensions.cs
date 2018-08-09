namespace DentalSystem.Web.Extensions
{
    using System;
    using DentalSystem.Data;
    using DentalSystem.Data.DbInitializer;
    using DentalSystem.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class WebHostExtensions
    {
        public static IWebHost SeedDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DentalSystemDbContext>();
                    //context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    context.Database.EnsureCreated();

                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    DbInitializer.Initialize(context, userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            return host;
        }
    }
}
