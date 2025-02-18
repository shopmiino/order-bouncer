using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly ICreateRequestBufferService _buffer;
    private readonly ILogger<OrderCreatedUseCase> _logger;
    public OrderCreatedUseCase(ILogger<OrderCreatedUseCase> logger, ICreateRequestBufferService buffer){
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
