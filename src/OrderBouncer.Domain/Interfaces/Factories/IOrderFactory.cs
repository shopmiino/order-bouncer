using System;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IOrderFactory
{
    public Order Create();
}
