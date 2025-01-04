using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
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
    private readonly IJsonExtractor _extractor;

    public OrderJsonMapping(IEntityFactory<OrderCreateDto, Order> orderFactory, IJsonMapping<ProductEntity> productMapping, IJsonExtractor extractor)
    {
        _orderFactory = orderFactory;
        _productMapping = productMapping;
        _extractor = extractor;
    }

    public Order? Map(string json)
    {
        JsonNode? node = _extractor.Extract(json);

        ICollection<ProductEntity>? products = _productMapping.MapMany(json);

        Order order = _orderFactory.Create(new (products));

        return order;
    }

    public ICollection<Order>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
