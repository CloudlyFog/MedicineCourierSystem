using Domain.OrderAggregate;
using Domain.RootAggregate;

namespace Domain.CourierAggregate;

public class Courier : Entity
{
    private string _name;

    public Courier(Guid id) : base(id)
    {
    }
    public Courier() : base()
    {
    }

    public string Name => _name;
    public Order? Order { get; private set; }
    public Guid? OrderId { get; private set; }
    public Courier UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return this;

        _name = name;
        return this;
    }

    public Courier UpdateOrder(Order order)
    {
        if (order is null)
            return this;

        Order = order;
        OrderId = order.Id;
        return this;
    }
}
