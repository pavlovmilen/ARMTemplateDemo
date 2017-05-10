using MyCoolWebApp.Data.Models;
using System.Data.Entity;

namespace MyCoolWebApp.Data
{
    public class MyCoolWebAppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.
        }
    }
}
