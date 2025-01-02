using System;
using System.Text.Json.Nodes;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class OrderJsonMapping
{
    private readonly IOrderFactory _orderFactory;
    public OrderJsonMapping(IOrderFactory orderFactory){
        _orderFactory = orderFactory;
    }

    public Order Map(string json){
        JsonNode node = JsonNode.Parse(json);
        
        throw new NotImplementedException();
    }
}
