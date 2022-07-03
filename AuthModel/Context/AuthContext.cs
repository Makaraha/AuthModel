using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthModel.Context
{
    public class AuthContext : IdentityDbContext<User>
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { } 
    }
}
