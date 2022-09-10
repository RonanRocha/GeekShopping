using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServer.Model.Context
{
    public class AppDataContext : IdentityDbContext<ApplicationUser>
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

    }
}
