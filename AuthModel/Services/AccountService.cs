using AuthModel.Context;
using AuthModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthModel.Services
{
    public class AccountService : ControllerBase
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
            var result = await userManager.CreateAsync(user, model.Password);

            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new IdentityRole() { Name = "user" });

            var result2 = await userManager.AddToRoleAsync(user, "user");
        }
    }
}
