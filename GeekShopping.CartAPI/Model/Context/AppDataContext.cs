using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Model.Context
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        {

        }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }

    }
}
