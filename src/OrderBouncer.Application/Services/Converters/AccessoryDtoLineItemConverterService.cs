using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class AccessoryDtoLineItemConverterService : ILineItemsConverterService<AccessoryDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemsBaseConverterService _baseConverter;
    private readonly ILogger<AccessoryDtoLineItemConverterService> _logger;

    public AccessoryDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter, ILogger<AccessoryDtoLineItemConverterService> logger){
        _extractor = extractor;
        _baseConverter = baseConverter;
        _logger = logger;
    }
    public async Task<AccessoryDto> Convert(LineItem lineItem, Guid scopeId)
    {
        _logger.LogInformation("Converting lineItem to AccessoryDto");
        BaseDto? baseDto = null;
        
        try{
            baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetAccessoryNotes, scopeId);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error while GenericConverting ACCESSORYDTO to BASEDTO");
        }

        if(baseDto is null){
            throw new ArgumentNullException("Converted BaseDto is null, there is a problem. Throwing.");
        }
        return baseDto.ToAccessoryDto();
    }

    public Task<(AccessoryDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(AccessoryDto, List<PetDto>?, List<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

}
