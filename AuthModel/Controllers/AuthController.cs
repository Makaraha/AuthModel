using AuthModel.Models;
using AuthModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthModel.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService authService { get; set; }
        private IHttpContextAccessor httpContextAccessor { get; set; }

        public AuthController(AuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            this.authService = authService;
            this.httpContextAccessor = httpContextAccessor;

        }


        [HttpPost]
        public async Task<LoginModel> Login(string name, string password)
        {
            return await authService.Login(name, password);
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public async Task<string> Test()
        {
            var test = httpContextAccessor.HttpContext.User.Claims.ToList();
            return await Task.FromResult("good");
        }
    }
}
