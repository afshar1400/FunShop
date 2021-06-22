using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.MVC.SeedData
{
    public static class SeedDataInitial
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("johndoe@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "johndoe@localhost";
                user.Email = "johndoe@localhost";
;

                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            string adminEmail = "admin@gmail.com";
            if (userManager.FindByEmailAsync(adminEmail).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = adminEmail;
                user.Email = adminEmail;


                IdentityResult result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
                Console.WriteLine(roleResult.Errors);
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
                Console.WriteLine(roleResult.Errors);
            }
        }
    }
}

