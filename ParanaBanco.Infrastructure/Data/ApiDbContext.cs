using Microsoft.EntityFrameworkCore;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Infrastructure.Mappings;

namespace ParanaBanco.Infrastructure.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().Map();
            modelBuilder.Entity<Phone>().Map();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Phone> Phones { get; set; }
    }
}
