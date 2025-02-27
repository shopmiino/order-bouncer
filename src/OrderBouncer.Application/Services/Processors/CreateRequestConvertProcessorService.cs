using System;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CreateRequestConvertProcessorService> _logger;

    public CreateRequestConvertProcessorService(IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto> requestConverter, IJobContext jobContext, ILogger<CreateRequestConvertProcessorService> logger){
        _requestConverter = requestConverter;
        _jobContext = jobContext;
        _logger = logger;
    }
    public async Task ConvertAndStoreAsync(OrderCreatedShopifyRequestDto request, Guid jobId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{0}'s ConvertAndStoreAsync is started with jobId: {1}\n{2} -> OrderDto\nIt's a chained process, first this and after {3}",nameof(CreateRequestConvertProcessorService), jobId, nameof(OrderCreatedShopifyRequestDto), nameof(CreateRequestProcessorService));

        OrderDto converted = await _requestConverter.Convert(request, jobId);
        
        _logger.LogDebug("Converting is done, trying to store converted OrderDto into JobContext with jobId: {0}", jobId);
        _jobContext.TryStoreObject<OrderDto>(jobId, converted);
    }
}
