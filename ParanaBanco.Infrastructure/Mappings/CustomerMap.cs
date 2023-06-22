using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBanco.Domain.Entities.Customers;

namespace ParanaBanco.Infrastructure.Mappings
{
    public static class CustomerMap
    {
        public static void Map(this EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder
                .HasKey(prop => prop.Id);
            builder.Property(prop => prop.FullName).HasColumnName("FullName").IsRequired();
            builder
                .HasMany(item => item.Phones);
        }
    }
}
