using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_Net_40_EF04.Data.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Type)
                   .HasConversion<string>();

            builder.HasIndex(c => c.NationalId)
                   .IsUnique();

            builder.HasMany(c => c.CustomerAccounts)
                   .WithOne(ca => ca.Customer)
                   .HasForeignKey(ca => ca.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
