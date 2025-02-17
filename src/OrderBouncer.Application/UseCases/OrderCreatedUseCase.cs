using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly IJsonMapping<Order> _orderMapping;
    private readonly ICreateRequestBufferService _buffer;
    private readonly IOutboxExecutor _outbox;
    private readonly ILogger<OrderCreatedUseCase> _logger;
    public OrderCreatedUseCase(IJsonMapping<Order> orderMapping, IOutboxExecutor outbox, ILogger<OrderCreatedUseCase> logger, ICreateRequestBufferService buffer){
        _orderMapping = orderMapping;
        _outbox = outbox;
        _logger = logger;
        _buffer = buffer;
    }
    public async Task<bool> ExecuteAsync(OrderCreatedShopifyRequestDto requestDto, CancellationToken cancellationToken)
    {   
        OrderDto orderDto = new(){};
        
        await _buffer.EnqueueAsync(orderDto,cancellationToken);

        //await _outbox.ExecuteAsync(orderDto, cancellationToken);
        //await _outbox.ExecutePathAsync(filePath, cancellationToken);
        return false;
    }
}
