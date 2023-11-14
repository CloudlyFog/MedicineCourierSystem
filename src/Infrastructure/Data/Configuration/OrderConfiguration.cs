using Domain.CourierAggregate;
using Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Medicines).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
        builder.HasOne(x => x.Courier).WithOne(x => x.Order).HasForeignKey<Courier>(x => x.OrderId);
        builder.ComplexProperty(x => x.Address);

        builder.Property(x => x.TotalCost)
            .HasField("_totalCost");
        builder
            .Property(x => x.OrderDate);
        builder
            .Property(x => x.PaymentMethod)
            .HasField("_paymentMethod");
        builder
            .Property(x => x.Status)
            .HasField("_status");
    }
}
