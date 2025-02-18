using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Options;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Variants;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemsProcessorService : ILineItemsProcessorService
{
    private readonly ShopifySettings _settings;
    private readonly ILineItemsProcessorHelperService _helper;

    public LineItemsProcessorService(IOptions<ShopifySettings> options, ILineItemsProcessorHelperService helper){
        _settings = options.Value;
        _helper = helper;
    }
    public async ValueTask<ProductDto> Process(LineItem[] lineItems)
    {
        //there should be LineItems based ProductDto Factory. We have to calculate which properties are gonna be null
       ProductDto productDto = new();

       foreach(var lineItem in lineItems){
            var selection = _settings.ProductIdTable.Single(p => p.ShopifyID == lineItem.ProductId);
            Type type = ProductMappings.ProductDtoPairs[(ShopifyProductsEnum)selection.InternalID];
            
            _helper.FilterAndAdd((ShopifyProductsEnum)selection.InternalID, lineItem, ref productDto);
       }

       return productDto;
    }
}
