using AuthModel.Context;
using AuthModel.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthModel.Services
{
    public class AccountService
    {
        private UserManager<User> userManager { get; set; }
        private RoleManager<IdentityRole> roleManager { get; set; }
        private AuthContext authContext { get; set; }

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AuthContext authContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authContext = authContext;
        }

        public async Task Create(UserModel model)
        {
            var user = new User();
            user.UserName = model.Name;
            await userManager.CreateAsync(user);
            await userManager.AddPasswordAsync(user, model.Password);

            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new IdentityRole() { Name = "user" });

            await userManager.AddToRoleAsync(user, "user");
            await authContext.SaveChangesAsync();
        }
    }
}
