using Domain.CourierAggregate;
using Domain.MedicineAggregate;
using Domain.RootAggregate;

namespace Domain.OrderAggregate;

public class Order : Entity
{
    public static readonly Order Default = new(Guid.Empty);
    
    private Status _status;
    private Paymentmethod _paymentMethod;
    private double _totalCost;
    private readonly List<Medicine> _medicines;

    public Order(Guid id) : base(id)
    {
        _status = Status.Pending;
        _paymentMethod = Paymentmethod.YooMoney;
        _medicines = new();
    }

    public Order(Guid id, Status status, Paymentmethod paymentMethod) : base(id)
    {
        _status = status;
        _paymentMethod = paymentMethod;
        _medicines = new();
    }

    public Order() : base()
    {
        _status = Status.Pending;
        _paymentMethod = Paymentmethod.YooMoney;
        _medicines = new();
    }
    public DateTime OrderDate { get; } = DateTime.UtcNow;
    public double TotalCost => _totalCost;
    public Paymentmethod PaymentMethod => _paymentMethod;
    public Status Status => _status;
    public Address Address { get; private set; }
    public Courier? Courier { get; private set; }
    public Guid? CourierId { get; private set; }
    public IReadOnlyCollection<Medicine> Medicines => _medicines;

    public Order AddMedicine(Medicine medicine)
    {
        if (medicine == null)
            return this;

        _medicines.Add(medicine);
        _totalCost = _medicines.Sum(x => x.Price);
        return this;
    }

    public Order UpdateStatus(Status status)
    {
        _status = status;
        return this;
    }

    public Order UpdatePaymentMethod(Paymentmethod paymentMethod)
    {
        _paymentMethod = paymentMethod;
        return this;
    }

    public Order UpdateAddress(Address address)
    {
        if (address == null)
            return this;

        Address = address;
        return this;
    }
}
