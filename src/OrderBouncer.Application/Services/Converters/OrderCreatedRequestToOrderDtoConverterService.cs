using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Options;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Interfaces.Factories;
using OrderBouncer.Domain.Variants;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Converters;

public class OrderCreatedRequestToOrderDtoConverterService : IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto>
{
    private readonly ShopifySettings _settings;
    private readonly ILineItemsProcessorService _linesProcessor;

    public OrderCreatedRequestToOrderDtoConverterService(IOptions<ShopifySettings> options, ILineItemsProcessorService linesProcessor){
        _settings = options.Value;
        _linesProcessor = linesProcessor;
    }
    public async Task<OrderDto> Convert(OrderCreatedShopifyRequestDto input, Guid scopeId)
    {
        if(input.LineItems is null) throw new ArgumentNullException("LineItems are null, can not process further");
        ProductDto productDto = await _linesProcessor.Process(input.LineItems, scopeId);

        DateTime createdAt = input.CreatedAt is null ? default(DateTime) : input.CreatedAt.Value.UtcDateTime;
        string orderName = input.Name ?? "NONAME";

        OrderDto orderDto = new(orderName, [productDto], input.Note, createdAt);
        orderDto.ScopeId = scopeId;
        
        return orderDto;
    }
}
