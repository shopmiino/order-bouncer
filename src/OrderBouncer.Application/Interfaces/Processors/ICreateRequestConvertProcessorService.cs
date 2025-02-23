using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Processors;

public interface ICreateRequestConvertProcessorService
{
    public Task<OrderDto> ConvertAsync(OrderCreatedShopifyRequestDto request, CancellationToken cancellationToken);
}
