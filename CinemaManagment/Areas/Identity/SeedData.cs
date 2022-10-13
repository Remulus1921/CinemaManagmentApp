using CinemaManagment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaManagment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CinemaManagment.Areas.Identity
{
    public class SeedData
    {
        private readonly ApplicationDbContext _context;

        public SeedData(ApplicationDbContext context)
        {
            _context = context;
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Manager" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task Initialize(
            IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager,
            string password, string password2)
        {

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
                {
                    //manager
                    var managerUid = await Seed(serviceProvider, "manager@demo.com", password, userManager, "Manager");
                    await EnsureRole(serviceProvider, userManager, managerUid, "Manager");

                    //admin
                    var adminUid = await Seed(serviceProvider, "admin@demo.com", password2, userManager, "Admin");
                    await EnsureRole(serviceProvider, userManager, adminUid, "Admin");
                }
        }

        private static async Task<string> Seed(IServiceProvider serviceProvider, string userName, string initPw, UserManager<ApplicationUser> userManager, string role)
        {
            //var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    FirstName = role,
                    LastName = role,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, initPw);
            }

            if (user == null)
                throw new Exception("User dod not get created. Password policy problem?");

            return user.Id;
        }
        
        private static async Task<IdentityResult> EnsureRole(
            IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager,
            string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            IdentityResult ir;

            if (await roleManager.RoleExistsAsync(role) == false)
            {
                ir = await roleManager.CreateAsync(new IdentityRole(role));
            }

            //var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
                throw new Exception("User not existing");

            ir = await userManager.AddToRoleAsync(user, role);

            return ir;
        }

    }
}
