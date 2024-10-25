using Microsoft.EntityFrameworkCore;
using ShopPlase.Models;

namespace ShopPlase.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {




        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Basket> Baskets { get; set; }

    }

}
