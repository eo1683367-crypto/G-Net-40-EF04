using System;
using System.Collections.Generic;
using System.Text;
using G_Net_40_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_Net_40_EF04.Data.Configurations
{
    public class BranchConfigurations : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasIndex(b => b.ManagerId)
                   .IsUnique();

            builder.HasIndex(b => b.Code)
                  .IsUnique();

            builder.HasOne(b => b.Manager)
                   .WithOne(b => b.Branch)
                   .HasForeignKey<Branch>(b => b.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
