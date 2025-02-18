using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsProcessorHelperService
{
    public void FilterAndAdd(ShopifyProductsEnum productEnum, LineItem lineItem, ref ProductDto productDto);
}
