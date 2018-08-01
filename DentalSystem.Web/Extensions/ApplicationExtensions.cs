namespace DentalSystem.Web.Extensions
{
    using System;
    using DentalSystem.Web.Contants;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static string ADMIN_EMAIL = "admin@gmail.com";

        public static void AddAdministrator(this IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var role = roleManager.FindByNameAsync(RolesContants.ADMINISTRATOR).Result;
            if (role == null)
            {
                var res = roleManager.CreateAsync(new IdentityRole(RolesContants.ADMINISTRATOR)).Result;
            }

            var user = userManager.FindByEmailAsync(ADMIN_EMAIL).Result;
            if (user != null)
            {
                var isInRole = userManager.IsInRoleAsync(user, RolesContants.ADMINISTRATOR).Result;
                if (!isInRole)
                {
                    var result = userManager.AddToRoleAsync(user, RolesContants.ADMINISTRATOR).Result;
                }
            }
            else
            {
                var createdUser =
                    userManager.CreateAsync(
                        new IdentityUser { Email = ADMIN_EMAIL, UserName = "Admin", EmailConfirmed = true }, 
                        "admin123").Result;

                user = userManager.FindByEmailAsync(ADMIN_EMAIL).Result;

                var result = userManager.AddToRoleAsync(user, RolesContants.ADMINISTRATOR).Result;
            }
        }
    }
}
