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
    private readonly ILineItemsConverterService<FigureDto> _singleFigureConverter;

    public LineItemsProcessorService(IOptions<ShopifySettings> options, ILineItemsConverterService<FigureDto> singleFigureConverter){
        _settings = options.Value;
        _singleFigureConverter = singleFigureConverter;
    }
    public async ValueTask<ProductDto> Process(LineItem[] lineItems)
    {
       foreach(var lineItem in lineItems){
            var selection = _settings.ProductIdTable.Single(p => p.ShopifyID == lineItem.ProductId);
            Type type = ProductMappings.ProductDtoPairs[(ShopifyProductsEnum)selection.InternalID];
            
            switch((ShopifyProductsEnum)selection.InternalID){
                case ShopifyProductsEnum.Miino_Pop:
                    FigureDto figureDto = _singleFigureConverter.Convert(lineItem);
                    break;
                case ShopifyProductsEnum.Cift_Miino_Popu:
                    
                    break;
                case ShopifyProductsEnum.Miino_Pop_Anahtarlik:

                    break;
                case ShopifyProductsEnum.Miino_Pop_Evcil_Hayvan:

                    break;
                case ShopifyProductsEnum.Miino_Pop_Aksesuar:

                    break;
            }
       }

       ProductDto productDto = new();
       return productDto;
    }
}
