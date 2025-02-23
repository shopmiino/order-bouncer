using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Processors;

public interface ICreateRequestConvertProcessorService
{
    public Task ConvertAndStoreAsync(OrderCreatedShopifyRequestDto request, Guid jobId, CancellationToken cancellationToken);
}
