using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class GoodHamburguerDbContext:DbContext
    {

        public GoodHamburguerDbContext(DbContextOptions option ) : base(option) { }

        public DbSet<Sandwiche> Sandwiches { get; set; }
        public DbSet<Extra> Extras { get; set; }

        public DbSet<SandwichesExtras> SandwichesExtras { get; set; }

        public DbSet<Order> Orders { get; set; }


    }
}
