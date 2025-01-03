using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class OrderJsonMapping : IJsonMapping<Order>
{
    private readonly IEntityFactory<OrderCreateDto, Order> _orderFactory;
    private readonly IJsonMapping<ProductEntity> _productMapping;


    public OrderJsonMapping(IEntityFactory<OrderCreateDto, Order> orderFactory, IJsonMapping<ProductEntity> productMapping)
    {
        _orderFactory = orderFactory;
        _productMapping = productMapping;
    }

    public Order? Map(string json)
    {
        JsonNode? node = JsonNode.Parse(json);

        ProductEntity[]? products = _productMapping.MapMany(json);

        Order order = _orderFactory.Create(new (products));

        return order;
    }

    public Order[]? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
