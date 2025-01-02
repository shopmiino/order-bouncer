using System;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class OrderFactory : IOrderFactory
{
    public Order Create()
    {
        throw new NotImplementedException();
    }
}
