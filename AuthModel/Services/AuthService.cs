using AuthModel.Context;
using AuthModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthModel.Services
{
    public class AuthService
    {
        private UserManager<User> userManager { get; set; }

        public AuthService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<LoginModel> Login(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user is null)
                throw new Exception("User does not exist");

            if(await userManager.CheckPasswordAsync(user, password))
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.Locality, password));

                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                    issuer: "me",
                    audience: "you",
                    notBefore: now,
                    claims: claims,
                    expires: now.AddMinutes(2),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretkey!123")), SecurityAlgorithms.HmacSha256));

                var textJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new LoginModel()
                {
                    Name = user.UserName,
                    Access = textJwt
                };
            }
            throw new Exception("Wrong password");
        }
    }
}
