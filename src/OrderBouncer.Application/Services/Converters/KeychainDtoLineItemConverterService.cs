using System;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<KeychainDtoLineItemConverterService> _logger;

    public KeychainDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemsBaseConverterService baseConverter, ILogger<KeychainDtoLineItemConverterService> logger){
        _extractor = extractor;
        _baseConverter = baseConverter;
        _logger = logger;
    }
    
    public async Task<KeychainDto> Convert(LineItem lineItem, Guid scopeId)
    {
        _logger.LogInformation("Converting lineItem to KeychainDto");
        BaseDto? baseDto = null;

        try{
            baseDto = await _baseConverter.GenericConvert(lineItem, _extractor.GetKeychainNotes, scopeId);
        } catch (Exception ex) {
            _logger.LogError("Error while GenericConverting KEYCHAINDTO to BASEDTO\nmessage: {0}\nstackTrace: {1}", ex.Message, ex.StackTrace);
        }

        if(baseDto is null){
            throw new ArgumentNullException("Converted BaseDto is null, there is a problem. Throwing.");
        }
        return baseDto.ToKeychainDto();
    }

    public Task<(KeychainDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(KeychainDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }
}
