using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Model.Context
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
    }
}
