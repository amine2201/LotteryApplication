using LotteryApplication.DBContext;
using LotteryApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace LotteryApplication.Data
{
    public class SeedData
    {
        public static async Task Intiliaze(IServiceProvider services)
        {

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Participant"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Participant"));
                }
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Create a new ApplicationUser and assign them the "admin" role
                var email = "admin@dvlottery.com";
                ApplicationUser adminUser = await userManager.FindByEmailAsync(email);
                if (adminUser == null)
                {
                var user = new ApplicationUser
                {
                    FirstName = "admin",
                    LastName = "admin",
                    IsAdmin = true,
                    Email = email,
                    UserName = "admin@dvlottery.com"
                };

                var result = await userManager.CreateAsync(user, "Password123.");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }

        
    }
}
