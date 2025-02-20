using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class AccessoryDtoLineItemConverterService : ILineItemsConverterService<AccessoryDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemsBaseConverterService _baseConverter;

    public AccessoryDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter){
        _extractor = extractor;
        _baseConverter = baseConverter;
    }
    public async Task<AccessoryDto> Convert(LineItem lineItem)
    {
        BaseDto baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetAccessoryNotes);
        return (AccessoryDto)baseDto;
    }

    public Task<(AccessoryDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

}
