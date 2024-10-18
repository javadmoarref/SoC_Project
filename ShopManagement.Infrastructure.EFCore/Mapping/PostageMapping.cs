using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.PostageAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class PostageMapping : IEntityTypeConfiguration<Postage>
    {
        public void Configure(EntityTypeBuilder<Postage> builder)
        {
            builder.ToTable("Postages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PostagePrice);
        }
    }
}
