﻿using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mappings;

public class AccountMapping:IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Fullname).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();
        builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.ProfilePhoto).HasMaxLength(500);
        builder.Property(x => x.PostalCode).HasMaxLength(10);

        builder.HasOne(x => x.Role).WithMany(x => x.Accounts)
            .HasForeignKey(x => x.RoleId);
    }
}