//using Microsoft.AspNetCore.Identity;

//namespace SalaryManagement.DAL
//{
//   public class DataSeedingInitializer
//   {
//      public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
//      {
//         string[] roles = { "Admin", "Manager", "Staff" };
//         foreach (var role in roles)
//         {
//            var roleExist = await roleManager.RoleExistsAsync(role);
//            if (!roleExist)
//            {
//               IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
//            }
//         }

//         // Create Admin User
//         if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
//         {
//            IdentityUser user = new IdentityUser
//            {
//               UserName = "admin@admin.com",
//               Email = "admin@admin.com"
//            };
//            IdentityResult identityResult = userManager.CreateAsync(user, "password1").Result;
//            if (identityResult.Succeeded)
//            {
//               userManager.AddToRoleAsync(user, "Admin").Wait();
//            }
//         }

//         // Create Manager User
//         if (userManager.FindByEmailAsync("manager@manager.com").Result == null)
//         {
//            IdentityUser user = new IdentityUser
//            {
//               UserName = "manager@manager.com",
//               Email = "manager@manager.com"
//            };
//            IdentityResult identityResult = userManager.CreateAsync(user, "password1").Result;
//            if (identityResult.Succeeded)
//            {
//               userManager.AddToRoleAsync(user, "Manager").Wait();
//            }
//         }

//         // Create Staff User
//         if (userManager.FindByEmailAsync("staff@staff.com").Result == null)
//         {
//            IdentityUser user = new IdentityUser
//            {
//               UserName = "staff@staff.com",
//               Email = "staff@staff.com"
//            };

//            IdentityResult identityResult = userManager.CreateAsync(user, "password1").Result;

//            if (identityResult.Succeeded)
//            {
//               userManager.AddToRoleAsync(user, "staff").Wait();
//            }
//         }

//         // Create No Role User
//         if (userManager.FindByEmailAsync("norole@norole.com").Result == null)
//         {
//            IdentityUser user = new IdentityUser
//            {
//               UserName = "norole@norole.com",
//               Email = "norole@norole.com"
//            };

//            IdentityResult identityResult = userManager.CreateAsync(user, "password1").Result;
//            // No ROle assigned to Mr No Role
//         }

//      }
//   }
//}
