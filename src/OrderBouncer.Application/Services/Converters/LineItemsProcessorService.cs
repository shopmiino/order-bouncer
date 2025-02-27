using System;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<LineItemsProcessorService> _logger;

    public LineItemsProcessorService(IOptions<ShopifySettings> options, ILineItemsProcessorHelperService helper, ILogger<LineItemsProcessorService> logger){
        _settings = options.Value;
        _helper = helper;
        _logger = logger;
    }
    public async ValueTask<ProductDto> Process(LineItem[] lineItems, Guid scopeId)
    {
        _logger.LogInformation("Process is started for ProductDto, lineItems -> ProductDto. {0} jobId", scopeId);
        //there should be LineItems based ProductDto Factory. We have to calculate which properties are gonna be null
        ProductDto productDto = new([],[],[],[]);
        _logger.LogDebug("ProductDto is initialized with empty collections");

        _logger.LogDebug("{0} iterations are starting for ProductDto Process", lineItems.Count());
        int iterationCount = 0;
        foreach(var lineItem in lineItems){
            _logger.LogDebug("Iteration {0} starting", iterationCount);
            var selection = _settings.ProductIdTable.Single(p => p.ShopifyID == lineItem.ProductId);
            Type type = ProductMappings.ProductDtoPairs[(ShopifyProductsEnum)selection.InternalID];
            
            productDto = await _helper.FilterAndAdd((ShopifyProductsEnum)selection.InternalID, lineItem, productDto, scopeId);
            iterationCount++;
        }
        _logger.LogTrace("Iterations finished, continuing with ProductDto creation");
        
        ICollection<FigureDto>? figureDtos = productDto.Figures.Count <= 0 ? null : productDto.Figures;
        _logger.LogTrace("Created FigureDto collection {0}", figureDtos is null ? "is null" : $"has {productDto.Figures.Count()} elements");

        ICollection<AccessoryDto>? accessoryDtos = productDto.Accessories.Count <= 0 ? null : productDto.Accessories;
        _logger.LogTrace("Created AccessoryDto collection {0}", accessoryDtos is null ? "is null" : $"has {productDto.Accessories.Count()} elements");

        ICollection<KeychainDto>? keychainDtos = productDto.Keychains.Count <= 0 ? null : productDto.Keychains;
        _logger.LogTrace("Created KeychainDto collection {0}", keychainDtos is null ? "is null" : $"has {productDto.Keychains.Count()} elements");

        ICollection<PetDto>? petDtos = productDto.Pets.Count <= 0 ? null : productDto.Pets;
        _logger.LogTrace("Created PetDto collection {0}", petDtos is null ? "is null" : $"has {productDto.Pets.Count()} elements");

        ProductDto finalProduct = new(
            figures: figureDtos,
            accessories: accessoryDtos,
            keychains: keychainDtos,
            pets: petDtos
        );

        _logger.LogDebug("Final ProductDto is created, returning");
        return finalProduct;
    }
}
