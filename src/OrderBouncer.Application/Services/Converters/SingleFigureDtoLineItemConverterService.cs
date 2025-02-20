using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
    private readonly IImageSaverService _imageSaver;
    private readonly ILineItemExtrasConverterService _extrasConverter;
    private readonly ILineItemConverterHelperService _helper;
    
    public SingleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, IImageSaverService imageSaver, ILineItemConverterHelperService helper, ILineItemExtrasConverterService extrasConverter)
    {
        _extractor = extractor;
        _imageSaver = imageSaver;
        _helper = helper;
        _extrasConverter = extrasConverter;
    }

    public async Task<FigureDto> Convert(LineItem lineItem){
        return new();
    }
    public async Task<(FigureDto, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
    
    public async Task<(FigureDto, PetDto?)> ConvertWithExtraPet(LineItem lineItem)
    {
        SingleFigureVariant variant = VariantMappings.SingleFigureVariantMappings[lineItem.VariantId];

        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }

        ICollection<string> imagePaths = [];

        AccessoryDto? accessoryDto = null;
        PetDto? petDto = null;
        int startPos = 0;

        NoteAttribute[]? figureNotes = _extractor.GetFigureNotes(lineItem.Properties);
        NoteAttribute[]? nameNotes = _extractor.GetNameNotes(lineItem.Properties);

        if(variant.HasExtraPet){
            petDto = (PetDto)await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetPetNotes, startPos);
            if(petDto.ImagePaths is not null) startPos++;
        }

        if (variant.HasExtraAccessory)
        {
            accessoryDto = (AccessoryDto)await _extrasConverter.ConvertExtra(lineItem, groupedImages, _extractor.GetAccessoryNotes, startPos);
            if(accessoryDto.ImagePaths is not null) startPos++;
        }

        for(int i = startPos; i < groupedImages.Count; i++){
            imagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[i], imagePaths);
        }

        FigureDto figureDto = new(
            accessoryDtos: accessoryDto is null ? null : [accessoryDto],
            imagePaths: imagePaths,
            note: figureNotes?[0].Value,
            name: nameNotes?[0].Value
        );

        return (figureDto, petDto);
    }

    public Task<(FigureDto, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }

    public Task<(FigureDto, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
}
