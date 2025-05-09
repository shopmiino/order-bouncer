using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class PetDtoLineItemConverterService : ILineItemsConverterService<PetDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemsBaseConverterService _baseConverter;
    private readonly ILogger<PetDtoLineItemConverterService> _logger;

    public PetDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter, ILogger<PetDtoLineItemConverterService> logger){
        _extractor = extractor;
        _baseConverter = baseConverter;
        _logger = logger;
    }

    public async Task<PetDto> Convert(LineItem lineItem, Guid scopeId)
    {
        _logger.LogInformation("Converting lineItem to PetDto");
        BaseDto? baseDto = null;
        
        try{
            baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetPetNotes, scopeId);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error while GenericConverting PETDTO to BASEDTO");
        }

        if(baseDto is null){
            throw new ArgumentNullException("Converted BaseDto is null, there is a problem. Throwing.");
        }
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

    public Task<(PetDto, List<PetDto>?, List<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }
}
