using System;
using Microsoft.Extensions.Options;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Options;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class OrderCreatedRequestToOrderDtoConverterService : IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto>
{
    public readonly ShopifySettings _settings;

    public OrderCreatedRequestToOrderDtoConverterService(IOptions<ShopifySettings> options){
        _settings = options.Value;
    }
    public async Task<OrderDto> Convert(OrderCreatedShopifyRequestDto input)
    {
        ICollection<FigureDto> figures = [];
        ICollection<AccessoryDto> accessories = [];
        ICollection<KeychainDto> keychains = [];
        ICollection<PetDto> pets = [];


        foreach(var item in _settings.ProductIdTable){
            input.LineItems.Where(p => p.ProductId == item.ShopifyID);
            item.ShopifyID
        }

        foreach(var lineItem in input.LineItems){
            var selection = _settings.ProductIdTable.Single(p => p.ShopifyID == lineItem.ProductId);    
            selection
        }

        FigureDto figureDto = new();
        AccessoryDto accessoryDto = new();
        KeychainDto keychainDto = new();
        PetDto petDto = new();

        ProductDto productDto = new([figureDto], [accessoryDto], [keychainDto], [petDto]);

        OrderDto orderDto = new(input.Name, [productDto], input.Note, input.CreatedAt.UtcDateTime);
        
        return orderDto;
    }

    private Func<int, ICollection<T>> Select<T>(){
        
    } 
}
