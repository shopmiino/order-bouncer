using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsProcessorHelperService
{
    public Task<ProductDto> FilterAndAdd(ShopifyProductsEnum productEnum, LineItem lineItem, ProductDto productDto, Guid scopeId);
}
