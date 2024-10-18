using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IssueTrackingNo).HasMaxLength(8);
            builder.Property(x => x.AccountId);
            builder.Property(x => x.AccountName).HasMaxLength(255);
            builder.Property(x => x.AccountMobile).HasMaxLength(20);
            builder.Property(x => x.TotalAmount);
            builder.Property(x => x.DiscountAmount);
            builder.Property(x => x.PayAmount);
            builder.Property(x => x.IsPaid);
            builder.Property(x => x.PostalCode);
            builder.Property(x => x.IsCanceled);
            builder.Property(x => x.IsPayUnSuccess);
            builder.Property(x => x.IsOrderCanceled);
            builder.Property(x => x.RefId);
            builder.Property(x => x.CreationDate);


            builder.OwnsMany(x => x.Items, NavigationBuilder =>
            {
                NavigationBuilder.ToTable("OrdetItems");
                NavigationBuilder.HasKey(x => x.Id);
                NavigationBuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
                NavigationBuilder.Property(x => x.ProductId);
                NavigationBuilder.Property(x => x.Name);
                NavigationBuilder.Property(x => x.Picture);
                NavigationBuilder.Property(x => x.Count);
                NavigationBuilder.Property(x => x.UnitPrice);
                NavigationBuilder.Property(x => x.DiscountRate);
              
            });
        }
    }
}
