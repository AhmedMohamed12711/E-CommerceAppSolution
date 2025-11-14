using DomainLayer.Models.Order_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.subtotal).HasColumnType("decimal(8,2)");
            builder.HasMany(o => o.items)
                .WithOne();
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany().HasForeignKey(o => o.DeliveryMethodId);
            builder.OwnsOne(o => o.shipToAddress);
        }
    }
}
