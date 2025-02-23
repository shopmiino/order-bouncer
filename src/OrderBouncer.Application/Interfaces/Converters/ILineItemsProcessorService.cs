using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsProcessorService
{
    public ValueTask<ProductDto> Process(LineItem[] lineItems, Guid scopeId);
}
