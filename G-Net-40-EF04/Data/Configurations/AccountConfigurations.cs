using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_Net_40_EF04.Data.Configurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasIndex(a => a.AccountNumber)
                   .IsUnique();

            builder.Property(a => a.AccountType)
                  .HasConversion<string>();


            builder.HasMany(a => a.CustomerAccounts)
                   .WithOne(ca => ca.Account)
                   .HasForeignKey(ca => ca.AccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
