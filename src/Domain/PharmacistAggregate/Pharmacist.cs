using Domain.PrescriptionAggregate;
using Domain.RootAggregate;

namespace Domain.PharmacistAggregate;

public class Pharmacist : Entity
{
    private string _name;
    private List<Prescription> _releasedPrescriptions;

    public Pharmacist(Guid id) : base(id)
    {
        _releasedPrescriptions = new();
    }
    public Pharmacist() : base()
    {
        _releasedPrescriptions = new();
    }
    public string Name => _name;
    public IReadOnlyCollection<Prescription> ReleasedPrescriptions => _releasedPrescriptions;
    public Pharmacist UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return this;

        _name = name;
        return this;
    }

    public Pharmacist AddPrescription(Prescription prescription)
    {
        if (prescription is null)
            return this;

        _releasedPrescriptions.Add(prescription);
        return this;
    }

    public Pharmacist UpdateReleasedPrescriptions(List<Prescription> prescriptions)
    {
        if (prescriptions?.Count <= 0)
            return this;

        _releasedPrescriptions = prescriptions;
        return this;
    }
}
