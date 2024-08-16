using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class FruitContext : DbContext
    {
        public FruitContext(DbContextOptions<FruitContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fruit>();
        }
        public DbSet<Fruit> Fruits { get; set; }
    }
}