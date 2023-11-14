using Domain.MedicineAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasField("_name");
        builder.Property(x => x.Price).HasField("_price");
        builder.Property(x => x.Description).HasField("_description");

        builder.HasOne(x => x.Prescription).WithOne(x => x.Medicine).HasForeignKey<Medicine>(x => x.PrescriptionId);
    }
}
