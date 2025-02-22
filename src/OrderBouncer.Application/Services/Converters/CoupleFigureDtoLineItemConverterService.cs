using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Variants;

namespace OrderBouncer.Application.Services.Converters;

public class CoupleFigureDtoLineItemConverterService : ILineItemsConverterService<FigureDto[]>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemExtrasConverterService _extrasConverter;
    private readonly ILineItemConverterHelperService _helper;
    private readonly ILogger<CoupleFigureDtoLineItemConverterService> _logger;

    private const int FIGURE_ITERATION_COUNT = 2;

    public CoupleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemExtrasConverterService extrasConverter, ILineItemConverterHelperService helper, ILogger<CoupleFigureDtoLineItemConverterService> logger){
        _extractor = extractor;
        _extrasConverter = extrasConverter;
        _helper = helper;
        _logger = logger;
    }

    public Task<FigureDto[]> Convert(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(FigureDto[], AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(FigureDto[], PetDto?)> ConvertWithExtraPet(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(FigureDto[], PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    //TODO add detailed logging
    public async Task<(FigureDto[], ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
    {
        _logger.LogInformation("Couple Figure Dto Line Item Converter's ConvertWithMultipleExtras starting.");

        if(lineItem.VariantId is null) throw new ArgumentNullException("VariantId is null");

        CoupleFigureVariant variant = VariantMappings.CoupleFigureVariantMappings[lineItem.VariantId.Value];
        
        if(lineItem.Properties is null) throw new ArgumentNullException("Properties are null");

        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }
        if(groupedImages.Count <= 0){
            throw new ArgumentNullException("No Grouped Images here, element is empty");
        }
        
        //pet photos
        //1st miino face photos
        //1st miino body photos
        //2nd miino face photos
        //2nd miino body photos

        //1st miino Accessory notes
        //1st miino Figure notes
        //2nd miino Accessory notes
        //2nd miino Figure notes

        FigureDto? firstFigure = null;
        FigureDto? secondFigure = null;
        _logger.LogDebug("First and second figure dtos iniliatized to null");

        ICollection<string> firstImagePaths = [];
        ICollection<string> secondImagePaths = [];
        _logger.LogDebug("First and second figure imagePath collections iniliatized to empty");

        AccessoryDto? firstAccessoryDto = null;
        AccessoryDto? secondAccessoryDto = null;
        _logger.LogDebug("First and second accessory dtos iniliatized to null");

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

            BaseDto baseDto = await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetPetNotes, startPos);
            petDto = baseDto.ToPetDto();

            if(petDto.ImagePaths is not null && petDto.ImagePaths.Count() > 0) {
                _logger.LogDebug("Pet's imagePaths are not null or empty, increasing startPos for next check");
                startPos++;
            }

            _logger.LogDebug("StartPos is {0}", startPos);
        }

        if(variant.HasExtraAccessoryForFirst){
            _logger.LogInformation("Variant has extra accessory for FIRST figure, processing");

            _logger.LogDebug("Starting convert with startPos: {0}, notePosition: {1}", startPos, 0);
            BaseDto baseDto = await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos, hasNoImage: true);
            firstAccessoryDto = baseDto.ToAccessoryDto();

            if(firstAccessoryDto.ImagePaths is not null && firstAccessoryDto.ImagePaths.Count() > 0) {
                _logger.LogDebug("First figure's accessory imagePaths are not null or empty, increasing startPos for next check");
                startPos++;
            }

            _logger.LogDebug("StartPos is {0}", startPos);
        }

        _logger.LogDebug("{0} iterations are starting for FIRST FIGURE's images. First iteration for head images, second iteration for body images", FIGURE_ITERATION_COUNT);
        for(int i = 0; i<FIGURE_ITERATION_COUNT; i++){
            _logger.LogDebug("Iteration: {0}, BatchImageSaveAndAdd starting with startPos: {1}", i, startPos);
            firstImagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[startPos], firstImagePaths);

            _logger.LogDebug("Image paths retrieved, increasing startPos count");
            startPos++;
            _logger.LogDebug("StartPos is {0}", startPos);
        }

        if(variant.HasExtraAccessoryForSecond){
            _logger.LogInformation("Variant has extra accessory for SECOND figure, processing");
            int notePosition = 1;

            _logger.LogDebug("Starting convert with startPos: {0}, notePosition: {1}", startPos, notePosition);
            BaseDto baseDto = await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos, notePosition, hasNoImage: true);
            secondAccessoryDto = baseDto.ToAccessoryDto();

            if(secondAccessoryDto.ImagePaths is not null && secondAccessoryDto.ImagePaths.Count() > 0) {
                _logger.LogDebug("Secnod figure's accessory imagePaths are not null or empty, increasing startPos for next check");
                startPos++;
            }

            _logger.LogDebug("StartPos is {0}", startPos);
        }

         _logger.LogDebug("{0} iterations are starting for SECOND FIGURE's images. First iteration for head images, second iteration for body images", FIGURE_ITERATION_COUNT);
        for(int i = 0; i<FIGURE_ITERATION_COUNT; i++){
            _logger.LogDebug("Iteration: {0}, BatchImageSaveAndAdd starting with startPos: {1}", i, startPos);
            secondImagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[startPos], secondImagePaths);

            _logger.LogDebug("Image paths retrieved, increasing startPos count");
            startPos++;
            _logger.LogDebug("StartPos is {0}", startPos);
        }

        _logger.LogDebug("Creating the FIRST FIGURE");

        string? firstFigureNote = figureNotes?[0].Value;
        _logger.LogTrace("FIRST FIGURE's note is {0}", firstFigureNote);

        string? firstFigureName = nameNotes?[0].Value;
        _logger.LogTrace("FIRST FIGURE's name is {0}", firstFigureName);

        ICollection<AccessoryDto>? firstAccessoryDtos = firstAccessoryDto is null ? null : [firstAccessoryDto];
        _logger.LogTrace("FIRST FIGURE's accessories is {0}", firstAccessoryDtos is null ? "null" : "not null");

        firstFigure = new(
            imagePaths: firstImagePaths,
            accessoryDtos: firstAccessoryDtos,
            note: firstFigureNote,
            name: firstFigureName
        );
        _logger.LogDebug("FIRST FIGURE created");


        _logger.LogDebug("Creating the SECOND FIGURE");
        string? secondFigureNote = figureNotes?[1].Value;
        _logger.LogTrace("SECOND FIGURE's note is {0}", secondFigureNote);

        string? secondFigureName = nameNotes?[1].Value;
        _logger.LogTrace("SECOND FIGURE's name is {0}", secondFigureName);

        ICollection<AccessoryDto>? secondAccessoryDtos = secondAccessoryDto is null ? null : [secondAccessoryDto];
        _logger.LogTrace("SECOND FIGURE's accessories is {0}", secondAccessoryDtos is null ? "null" : "not null");
        
        secondFigure = new(
            imagePaths: secondImagePaths,
            accessoryDtos: secondAccessoryDtos,
            note: secondFigureNote,
            name: secondFigureName
        );
        _logger.LogDebug("SECOND FIGURE created");

        return ([firstFigure, secondFigure], petDto is null ? null : [petDto], null);
    }
}
