using System;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class OrderFactory : IEntityFactory<OrderCreateDto, Order>
{
    public Order Create(OrderCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
