using Domain.PrescriptionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Pharmacist).WithMany(x => x.ReleasedPrescriptions).HasForeignKey(x => x.PharmacistId);
        builder.Property(x => x.Snils).HasField("_snils");
        builder.Property(x => x.DateBegin).HasField("_dateBegin");
        builder.Property(x => x.DateEnd).HasField("_dateEnd");
    }
}
