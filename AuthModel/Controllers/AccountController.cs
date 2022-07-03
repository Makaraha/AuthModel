using AuthModel.Models;
using AuthModel.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthModel.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController
    {
        private AccountService accountService { get; set; }

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }


        [HttpPost]
        public async Task CreateAsync(UserModel model)
        {
            await accountService.Create(model);
        }
    }
}
