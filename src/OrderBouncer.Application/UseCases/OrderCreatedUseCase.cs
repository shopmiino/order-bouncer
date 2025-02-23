using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly ICreateRequestBufferService _buffer;
    private readonly ILogger<OrderCreatedUseCase> _logger;
    private readonly IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> _requestConverter;
    public OrderCreatedUseCase(ILogger<OrderCreatedUseCase> logger, ICreateRequestBufferService buffer, IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> requestConverter){
        _logger = logger;
        _buffer = buffer;
        _requestConverter = requestConverter;
    }
    public async Task<bool> ExecuteAsync(OrderCreatedShopifyRequestDto requestDto, CancellationToken cancellationToken)
    {   
        _logger.LogInformation("Executing OrderCreatedUseCase");

        Guid scopeId = Guid.NewGuid();
        _logger.LogDebug("{0} job is running and converting", scopeId);
        
        OrderDto orderDto = await _requestConverter.Convert(requestDto, scopeId);
        
        await _buffer.EnqueueAsync(orderDto,cancellationToken);

        return false;
    }
}
