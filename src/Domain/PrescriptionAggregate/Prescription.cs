using Domain.MedicineAggregate;
using Domain.PharmacistAggregate;
using Domain.RootAggregate;

namespace Domain.PrescriptionAggregate;

public class Prescription : Entity
{
    private string _snils;
    private DateTime _dateBegin;
    private DateTime _dateEnd;

    public Prescription(Guid id) : base(id)
    {
    }

    public Prescription() : base()
    {
    }

    public string? Snils => _snils;
    public DateTime DateBegin => _dateBegin;
    public DateTime DateEnd => _dateEnd;

    public Pharmacist? Pharmacist { get; private set; }
    public Guid? PharmacistId { get; private set; }
    public Medicine? Medicine { get; private set; }
    public Guid? MedicineId { get; private set; }

    public Prescription UpdateSnils(string snils)
    {
        if (string.IsNullOrWhiteSpace(snils))
            return this;

        _snils = snils;
        return this;
    }

    public Prescription UpdatePharmacist(Pharmacist pharmacist)
    {
        if (pharmacist is null)
            return this;

        Pharmacist = pharmacist;
        PharmacistId = pharmacist.Id;
        return this;
    }

    public Prescription UpdateDate(DateTime dateBegin, DateTime dateEnd)
    {
        _dateBegin = dateBegin;
        _dateEnd = dateEnd;
        return this;
    }
}
