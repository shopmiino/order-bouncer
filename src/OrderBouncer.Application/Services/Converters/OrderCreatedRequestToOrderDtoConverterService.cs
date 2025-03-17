using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Normalizers;
using OrderBouncer.Application.Options;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Interfaces.Factories;
using OrderBouncer.Domain.Variants;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Converters;

public class OrderCreatedRequestToOrderDtoConverterService : IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto>
{
    private readonly ILineItemsNormalizer _normalizer;
    private readonly ILineItemsProcessorService _linesProcessor;
    private readonly ILogger<OrderCreatedRequestToOrderDtoConverterService> _logger;

    public OrderCreatedRequestToOrderDtoConverterService(ILineItemsNormalizer normalizer, ILineItemsProcessorService linesProcessor, ILogger<OrderCreatedRequestToOrderDtoConverterService> logger){
        _normalizer = normalizer;
        _linesProcessor = linesProcessor;
        _logger = logger;
    }
    public async Task<OrderDto> Convert(OrderCreatedShopifyRequestDto input, Guid scopeId)
    {
        _logger.LogInformation("{0} Convert {1} to {2} is started with {3} jobId", nameof(OrderCreatedRequestToOrderDtoConverterService), nameof(OrderCreatedShopifyRequestDto), nameof(OrderDto), scopeId);

        if(input.LineItems is null) throw new ArgumentNullException("LineItems are null, can not process further");

        _logger.LogDebug("Normalizing lineItems");
        LineItem[] normalizedLineItems = _normalizer.Normalize(input.LineItems);

        ProductDto productDto = await _linesProcessor.Process(normalizedLineItems, scopeId);

        DateTime createdAt = input.CreatedAt is null ? default(DateTime) : input.CreatedAt.Value.UtcDateTime;
        string orderName = input.Name ?? "NONAME";

        OrderDto orderDto = new(orderName, [productDto], input.Note, createdAt);
        orderDto.ScopeId = scopeId;
        
        _logger.LogDebug("{0} jobId converted and returning", scopeId);
        return orderDto;
    }
}
