using System;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class KeychainDtoLineItemConverterService : ILineItemsConverterService<KeychainDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemsBaseConverterService _baseConverter;

    public KeychainDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter){
        _extractor = extractor;
        _baseConverter = baseConverter;
    }
    
    public async Task<KeychainDto> Convert(LineItem lineItem)
    {
        BaseDto baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetKeychainNotes);
        return baseDto.ToKeychainDto();
    }

    public Task<(KeychainDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
}
