using Microsoft.AspNetCore.Identity;

namespace SpeakingInBitsWeb.Models
{
#nullable disable
    public static class IdentityHelper
    {
        // Add the necessary constants
        public const string Instructor = "Instructor";
        public const string Student = "Student";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();
       
            foreach(string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }        
        }

        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no users are present, make a default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
            if(numUsers == 0)
            {
                var defaultUser = new IdentityUser()
                {
                    Email = "Instructor@speakinginbits.com",
                    UserName = "Admin"
                };

                await userManager.CreateAsync(defaultUser, "Programmer01#");

                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
