using DomainLayer.Models.Order_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configurations
{
    public class DeliviryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Cost).HasColumnType("decimal(8,2)");
            //---
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(50);
            //--
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);
            //---
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(100);


        }
    }
}
