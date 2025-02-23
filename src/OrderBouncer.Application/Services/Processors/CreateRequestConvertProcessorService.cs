using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Context;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestConvertProcessorService : ICreateRequestConvertProcessorService
{
    private readonly IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> _requestConverter;
    private readonly IJobContext _jobContext;
    public CreateRequestConvertProcessorService(IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> requestConverter, IJobContext jobContext){
        _requestConverter = requestConverter;
        _jobContext = jobContext;
    }
    public async Task ConvertAndStoreAsync(OrderCreatedShopifyRequestDto request, Guid jobId, CancellationToken cancellationToken)
    {
        OrderDto converted = await _requestConverter.Convert(request, jobId);
        _jobContext.TryStoreObject<OrderDto>(jobId, converted);
    }
}
