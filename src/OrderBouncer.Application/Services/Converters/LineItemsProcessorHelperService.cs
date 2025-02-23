using System;
using System.Threading.Tasks;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Domain.DTOs.Base;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemsProcessorHelperService : ILineItemsProcessorHelperService
{
    private readonly ILineItemsConverterService<FigureDto> _singleFigureConverter;
    private readonly ILineItemsConverterService<FigureDto[]> _coupleFigureConverter;
    private readonly ILineItemsConverterService<KeychainDto> _keychainConverter;
    private readonly ILineItemsConverterService<PetDto> _petConverter;
    private readonly ILineItemsConverterService<AccessoryDto> _accessoryConverter;
    public LineItemsProcessorHelperService(
        ILineItemsConverterService<FigureDto> singleFigureConverter,
        ILineItemsConverterService<KeychainDto> keychainConverter,
        ILineItemsConverterService<PetDto> petConverter,
        ILineItemsConverterService<AccessoryDto> accessoryConverter,
        ILineItemsConverterService<FigureDto[]> coupleFigureConverter)
    {
        _singleFigureConverter = singleFigureConverter;
        _keychainConverter = keychainConverter;
        _petConverter = petConverter;
        _accessoryConverter = accessoryConverter;
        _coupleFigureConverter = coupleFigureConverter;
    }

    public async Task<ProductDto> FilterAndAdd(ShopifyProductsEnum productEnum, LineItem lineItem, ProductDto productDto, Guid scopeId)
    {
        string NullErrorString(string collectionName)
        {
            return $"ProductDto's {collectionName} Collection is null and you are trying to Add element to that null collection";
        }

        switch (productEnum)
        {
            case ShopifyProductsEnum.Miino_Pop:
                if (productDto.Figures is null)
                {
                    throw new ArgumentNullException(NullErrorString(nameof(productDto.Figures)));
                }
                (FigureDto figureDto, PetDto? petDto) result = await _singleFigureConverter.ConvertWithExtraPet(lineItem, scopeId);
                productDto.Figures.Add(result.figureDto);

                if(result.petDto is not null) productDto.Pets.Add(result.petDto);
                break;
            case ShopifyProductsEnum.Cift_Miino_Popu:
                if (productDto.Figures is null)
                {
                    throw new ArgumentNullException(NullErrorString(nameof(productDto.Figures)));
                }

                (FigureDto[] figures, ICollection<PetDto>? pets, ICollection<AccessoryDto>? accessories) coupleResult = await _coupleFigureConverter.ConvertWithMultipleExtras(lineItem, scopeId);

                productDto.Figures.Add(coupleResult.figures[0]);
                productDto.Figures.Add(coupleResult.figures[1]);

                if(coupleResult.pets is not null){
                    foreach(var pet in coupleResult.pets){
                        productDto.Pets.Add(pet);
                    }
                }
                break;
            case ShopifyProductsEnum.Miino_Pop_Anahtarlik:
                if (productDto.Keychains is null)
                {
                    throw new ArgumentNullException(NullErrorString(nameof(productDto.Keychains)));
                }
                
                productDto.Keychains.Add(await _keychainConverter.Convert(lineItem, scopeId));
                break;
            case ShopifyProductsEnum.Miino_Pop_Evcil_Hayvan:
                if (productDto.Pets is null)
                {
                    throw new ArgumentNullException(NullErrorString(nameof(productDto.Pets)));
                }

                productDto.Pets.Add(await _petConverter.Convert(lineItem, scopeId));
                break;
            case ShopifyProductsEnum.Miino_Pop_Aksesuar:
                if (productDto.Accessories is null)
                {
                    throw new ArgumentNullException(NullErrorString(nameof(productDto.Accessories)));
                }

                productDto.Accessories.Add(await _accessoryConverter.Convert(lineItem, scopeId));
                break;
        }

        return productDto;
    }
}
