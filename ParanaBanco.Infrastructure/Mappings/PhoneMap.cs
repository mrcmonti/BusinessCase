using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParanaBanco.Domain.Entities.Customers;
using ParanaBanco.Domain.Entities.Phones;
using ParanaBanco.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Infrastructure.Mappings
{
    public static class PhoneMap
    {
        public static void Map(this EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone");

            builder
                .HasKey(prop => prop.Id);
            builder.Property(prop => prop.AreaCode).HasColumnName("AreaCode").IsRequired();
            builder.Property(prop => prop.PhoneNumber).HasColumnName("PhoneNumber").IsRequired();

            builder.Property(prop => prop.Type)
                .HasConversion(p => p.ToString(), p =>
                (PhoneType)Enum.Parse(typeof(PhoneType), p))
                .HasColumnName("Type");

            builder.HasOne<Customer>()
                .WithMany(Customer => Customer.Phones)
                .HasForeignKey(item => item.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
