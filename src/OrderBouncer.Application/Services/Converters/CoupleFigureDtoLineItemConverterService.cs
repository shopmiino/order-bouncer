using System;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.HttpClients;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Variants;

namespace OrderBouncer.Application.Services.Converters;

public class CoupleFigureDtoLineItemConverterService : ILineItemsConverterService<FigureDto[]>
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemExtrasConverterService _extrasConverter;
    private readonly ILineItemConverterHelperService _helper;

    public CoupleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemExtrasConverterService extrasConverter, ILineItemConverterHelperService helper){
        _extractor = extractor;
        _extrasConverter = extrasConverter;
        _helper = helper;
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

    public async Task<(FigureDto[], ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
    {
        CoupleFigureVariant variant = VariantMappings.CoupleFigureVariantMappings[lineItem.VariantId];
                
        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
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

        ICollection<string> firstImagePaths = [];
        ICollection<string> secondImagePaths = [];

        AccessoryDto? firstAccessoryDto = null;
        AccessoryDto? secondAccessoryDto = null;

        PetDto? petDto = null;

        NoteAttribute[]? figureNotes = _extractor.GetFigureNotes(lineItem.Properties);
        NoteAttribute[]? nameNotes = _extractor.GetNameNotes(lineItem.Properties);

        int startPos = 0;

        if(variant.HasExtraPet){
            petDto = (PetDto)await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetPetNotes, startPos);
            if(petDto.ImagePaths is not null) startPos++;
        }

        if(variant.HasExtraAccessoryForFirst){
            firstAccessoryDto = (AccessoryDto)await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos);
            if(firstAccessoryDto.ImagePaths is not null) startPos++;
        }

        //first figure's photos
        for(int i = 0; i<2; i++){
            //first iteration for head images, second iteration for body images
            firstImagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[startPos], firstImagePaths);
            startPos++;
        }

        if(variant.HasExtraAccessoryForSecond){
            secondAccessoryDto = (AccessoryDto)await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos, 1);
            if(secondAccessoryDto.ImagePaths is not null) startPos++;
        }

        //second figure's photos
        for(int i = 0; i<2; i++){
            secondImagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[startPos], secondImagePaths);
            startPos++;
        }

        firstFigure = new(
            imagePaths: firstImagePaths,
            accessoryDtos: firstAccessoryDto is null ? null : [firstAccessoryDto],
            note: figureNotes?[0].Value,
            name: nameNotes?[0].Value
        );

        secondFigure = new(
            imagePaths: secondImagePaths,
            accessoryDtos: secondAccessoryDto is null ? null : [secondAccessoryDto],
            note: figureNotes?[1].Value,
            name: nameNotes?[1].Value
        );

        return ([firstFigure, secondFigure], petDto is null ? null : [petDto], null);
    }
}
