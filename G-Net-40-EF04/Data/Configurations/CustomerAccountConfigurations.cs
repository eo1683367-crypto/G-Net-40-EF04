using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_Net_40_EF04.Data.Configurations
{
    public class CustomerAccountConfigurations : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(ca => new
            {
                ca.AccountId,
                ca.CustomerId
            });

            builder.Property(ca => ca.ownershipType)
                  .HasConversion<string>();

            builder.Property(ca => ca.AccountStatus)
                  .HasConversion<string>();
        }
    }
}
