namespace DentalSystem.Web.Extensions
{
    using System;
    using System.Linq;
    using DentalSystem.Common.Contants;
    using DentalSystem.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static string ADMIN_EMAIL = "admin@gmail.com";
        public static string ADMIN_NAME = "Dimitar Radenkov";
        public static string ADMIN_PASS = "admin123";

        public static void AddRolesAndAdmin(this IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            if (roleManager.Roles.Any())
            {
                return;
            }

            //create Roles
            var roles = new[] { Roles.ADMINISTRATOR, Roles.DOCTOR };
            foreach (var role in roles)
            {
                var identityRole = roleManager.FindByNameAsync(role).Result;
                if (identityRole == null)
                {
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
                }
            }

            //create user
            userManager.CreateAsync(
                new User { Email = ADMIN_EMAIL, UserName = ADMIN_EMAIL, Name = ADMIN_NAME, EmailConfirmed = true },
                ADMIN_PASS).Wait();

            //add to roles
            var user = userManager.FindByEmailAsync(ADMIN_EMAIL).Result;
            userManager.AddToRolesAsync(user, roles).Wait();
        }
    }
}
