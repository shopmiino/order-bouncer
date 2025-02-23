using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.HttpClients;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Variants;

namespace OrderBouncer.Application.Services.Converters;

public class SingleFigureDtoLineItemConverterService : ILineItemsConverterService<FigureDto>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemExtrasConverterService _extrasConverter;
    private readonly ILineItemConverterHelperService _helper;
    private readonly ILogger<SingleFigureDtoLineItemConverterService> _logger;

    private const int FIGURE_ITERATION_COUNT = 2;
    
    public SingleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemConverterHelperService helper, ILineItemExtrasConverterService extrasConverter, ILogger<SingleFigureDtoLineItemConverterService> logger)
    {
        _extractor = extractor;
        _helper = helper;
        _extrasConverter = extrasConverter;
        _logger = logger;
    }

    public async Task<FigureDto> Convert(LineItem lineItem, Guid scopeId){
        return new();
    }
    public async Task<(FigureDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }
    
    public async Task<(FigureDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem, Guid scopeId)
    {
        _logger.LogInformation("Single Figure Dto Line Item Converter's ConvertWithExtraPet starting.");

        if(lineItem.VariantId is null) throw new ArgumentNullException("VariantId is null");

        SingleFigureVariant variant = VariantMappings.SingleFigureVariantMappings[lineItem.VariantId.Value];

        if(lineItem.Properties is null) throw new ArgumentNullException("Properties are null");

        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }

        ICollection<string> imagePaths = [];
        _logger.LogDebug("ImagePath collection initialized to empty");

        AccessoryDto? accessoryDto = null;
        _logger.LogDebug("Accessory dto initialized to null");

        PetDto? petDto = null;
        _logger.LogDebug("Petdto iniliatized to null");

        NoteAttribute[]? figureNotes = _extractor.GetFigureNotes(lineItem.Properties);
        _logger.LogDebug("Figure notes got from extractor. Total of {0}", figureNotes?.Count());

        NoteAttribute[]? nameNotes = _extractor.GetNameNotes(lineItem.Properties);
        _logger.LogDebug("Name notes got from extractor. Total of {0}", nameNotes?.Count());

        int startPos = 0;
        _logger.LogDebug("StartPos is {0}", startPos);

        if(variant.HasExtraPet){
            _logger.LogInformation("Variant has extra pet, processing");

            BaseDto baseDto = await _extrasConverter.ConvertExtra(scopeId, lineItem, groupedImages, _extractor.GetPetNotes, startPos);
            petDto = baseDto.ToPetDto();

            if(petDto.ImagePaths is not null && petDto.ImagePaths.Count() > 0) {
                _logger.LogDebug("Pet's imagePaths are not null or empty, increasing startPos for next check");
                startPos++;
            }

            _logger.LogDebug("StartPos is {0}", startPos);
        }

        if (variant.HasExtraAccessory)
        {
            _logger.LogInformation("Variant has extra accessory, processing");

            _logger.LogDebug("Starting convert with startPos: {0}, notePosition: {1}", startPos, 0);
            BaseDto baseDto = await _extrasConverter.ConvertExtra(scopeId, lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos, hasNoImage: true);
            accessoryDto = baseDto.ToAccessoryDto();

            if(accessoryDto.ImagePaths is not null && accessoryDto.ImagePaths.Count() > 0) {
                _logger.LogDebug("Accessory imagePaths are not null or empty, increasing startPos for next check");
                startPos++;
            }

            _logger.LogDebug("StartPos is {0}", startPos);
        }

        _logger.LogDebug("{0} iterations are starting for figure images. First iteration for head images, second iteration for body images", FIGURE_ITERATION_COUNT);
        for(int i = 0; i<FIGURE_ITERATION_COUNT; i++){
            _logger.LogDebug("Iteration: {0}, BatchImageSaveAndAdd starting with startPos: {1}", i, startPos);
            imagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[startPos], imagePaths, scopeId);

            _logger.LogDebug("Image paths retrieved, increasing startPos count");
            startPos++;
            _logger.LogDebug("StartPos is {0}", startPos);
        }

        _logger.LogDebug("Creating the FIGURE");

        string? figureNote = figureNotes?[0].Value;
        _logger.LogTrace("FIGURE's note is {0}", figureNote);

        string? figureName = nameNotes?[0].Value;
        _logger.LogTrace("FIGURE's name is {0}", figureName);

        FigureDto figureDto = new(
            imagePaths: imagePaths,
            accessoryDtos: accessoryDto is null ? null : [accessoryDto],
            note: figureNote,
            name: figureName
        );

        return (figureDto, petDto);
    }

    public Task<(FigureDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
    }

    public Task<(FigureDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId)
    {
        throw new NotImplementedException();
        }
}
