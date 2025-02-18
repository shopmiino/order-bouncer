using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Options;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Interfaces.Factories;
using OrderBouncer.Domain.Variants;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Converters;

public class OrderCreatedRequestToOrderDtoConverterService : IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto>
{
    private readonly ShopifySettings _settings;
    private readonly IFactory<LineItem, FigureDto> _singleFigureFactory;
    private readonly ILineItemsProcessorService _linesProcessor;

    private Dictionary<ShopifyProductsEnum, Type> _productMapping = [];

    public OrderCreatedRequestToOrderDtoConverterService(IOptions<ShopifySettings> options, ILineItemsProcessorService linesProcessor){
        _settings = options.Value;
        _linesProcessor = linesProcessor;
    }
    public async Task<OrderDto> Convert(OrderCreatedShopifyRequestDto input)
    {
        ICollection<FigureDto> figures = [];
        ICollection<AccessoryDto> accessories = [];
        ICollection<KeychainDto> keychains = [];
        ICollection<PetDto> pets = [];

        ProductDto productDto = await _linesProcessor.Process(input.LineItems);
        

        FigureDto figureDto = new();
        AccessoryDto accessoryDto = new();
        KeychainDto keychainDto = new();
        PetDto petDto = new();

        //ProductDto productDto = new([figureDto], [accessoryDto], [keychainDto], [petDto]);

        OrderDto orderDto = new(input.Name, [productDto], input.Note, input.CreatedAt.UtcDateTime);
        
        return orderDto;
    }
}
