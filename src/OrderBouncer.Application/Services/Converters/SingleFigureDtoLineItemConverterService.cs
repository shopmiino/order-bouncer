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
    private readonly ILineItemConverterHelperService _helper;
    public SingleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, IImageSaverService imageSaver, ILineItemConverterHelperService helper)
    {
        _extractor = extractor;
        _imageSaver = imageSaver;
        _helper = helper;
    }

    //TODO: I need to lookup for pet, if they add pet to the order, what I shoul do to include it.
    public async Task<FigureDto> Convert(LineItem lineItem)
    {
        SingleFigureVariant variant = VariantMappings.SingleFigureVariantMappings[lineItem.VariantId];

        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }

        ICollection<string> imagePaths = [];

        AccessoryDto? accessoryDto = null;
        int startPos = 0;

        NoteAttribute[]? figureNotes = _extractor.GetFigureNotes(lineItem.Properties);

        if (variant.HasExtraAccessory)
        {
            startPos++;

            NoteAttribute[]? accessoryNotes = _extractor.GetAccessoryNotes(lineItem.Properties);
            ICollection<string>? accessoryImages = [];

            if(groupedImages[0] is null || groupedImages[0].Length == 0) goto NoAccessoryImage;

            accessoryImages = await _helper.BatchImageSaveAndAdd(groupedImages[0], accessoryImages);

            accessoryDto = new(imagePaths: accessoryImages, note: accessoryNotes?[0].Value);

        }
        NoAccessoryImage:

        for(int i = startPos; i < groupedImages.Count; i++){
            imagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[i], imagePaths);
        }

        FigureDto figureDto = new(
            accessoryDtos: accessoryDto is null ? null : [accessoryDto],
            imagePaths: imagePaths
        );

        return figureDto;
    }
}
