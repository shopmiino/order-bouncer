using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class PetDtoLineItemConverterService : ILineItemsConverterService<PetDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemsBaseConverterService _baseConverter;

    public PetDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter){
        _extractor = extractor;
        _baseConverter = baseConverter;
    }

    public async Task<PetDto> Convert(LineItem lineItem, Guid scopeId)
    {
        BaseDto baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetPetNotes, scopeId);
        return baseDto.ToPetDto();
    }

    public Task<(PetDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(PetDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(PetDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(PetDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }
}
