using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestConvertProcessorService : ICreateRequestConvertProcessorService
{
    private readonly IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> _requestConverter;
    public CreateRequestConvertProcessorService(IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> requestConverter){
        _requestConverter = requestConverter;
    }
    public async Task<OrderDto> ConvertAsync(OrderCreatedShopifyRequestDto request, CancellationToken cancellationToken)
    {
        Guid jobId = Guid.NewGuid();
        return await _requestConverter.Convert(request, jobId);
    }
}
