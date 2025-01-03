using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class OrderJsonMapping : IJsonMapping<Order>
{
    private readonly IOrderFactory _orderFactory;
    private readonly IJsonMapping<ProductEntity> _productMapping;


    public OrderJsonMapping(IOrderFactory orderFactory, IJsonMapping<ProductEntity> productMapping)
    {
        _orderFactory = orderFactory;
        _productMapping = productMapping;
    }

    public Order Map(string json)
    {
        JsonNode node = JsonNode.Parse(json);

        Order order = _orderFactory.Create();
        _productMapping.Map(json);


        return order;
    }

    public Order[] MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
