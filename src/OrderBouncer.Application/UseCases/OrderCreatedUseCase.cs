using System;
using System.Text;
using System.Text.Json.Nodes;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.GoogleDrive;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly IJsonMapping<Order> _orderMapping;
    private readonly IOutboxExecutor _outbox;
    public OrderCreatedUseCase(IJsonMapping<Order> orderMapping, IOutboxExecutor outbox){
        _orderMapping = orderMapping;
        _outbox = outbox;
    }
    public async Task<bool> ExecuteAsync(string json, CancellationToken cancellationToken)
    {
        //JsonNode? node = JsonNode.Parse(json);
        //if(node is null){
        //    return false;
        //}
        //Order? order = await _orderMapping.Map(node);
        //DriveUploadDto dto = new ();
        
        //Save to db
        
        await _outbox.ExecuteAsync(Encoding.UTF8.GetBytes(json), cancellationToken);
        return false;
    }
}
