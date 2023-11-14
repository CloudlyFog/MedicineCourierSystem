using Domain.OrderAggregate;
using Domain.PrescriptionAggregate;
using Domain.RootAggregate;

namespace Domain.MedicineAggregate;

public class Medicine : Entity
{
    private string _name;
    private double _price;
    private string _description;
    public Medicine(Guid id) : base(id)
    {
    }

    public Medicine() : base()
    {
    }

    public string Name => _name;
    public double Price => _price;
    public string Description => _description;
    public Order? Order { get; private set; }
    public Guid? OrderId { get; private set; }
    public Prescription? Prescription { get; private set; }
    public Guid? PrescriptionId { get; private set; }

    public Medicine UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return this;
        _name = name;
        return this;
    }

    public Medicine UpdatePrice(double price)
    {
        if (price < 0)
            return this;

        _price = price;
        return this;
    }

    public Medicine UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return this;
        _description = description;
        return this;
    }

    public Medicine UpdateOrder(Order order)
    {
        if (order is null)
            return this;

        Order = order;
        OrderId = order.Id;
        return this;
    }

    public Medicine UpdatePrescription(Prescription prescription)
    {
        if (prescription is null)
            return this;

        Prescription = prescription;
        PrescriptionId = prescription.Id;
        return this;
    }
}
