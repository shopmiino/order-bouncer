using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class BaseDtoLineItemConverterService : ILineItemsBaseConverterService
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemConverterHelperService _helper;

    public BaseDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemConverterHelperService helper){
        _extractor = extractor;
        _helper = helper;
    }

    public async Task<BaseDto> GenericConvert(LineItem lineItem, Func<NoteAttribute[], NoteAttribute[]?> noteGetter)
    {
        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }

        ICollection<string> imagePaths = [];
        NoteAttribute[]? notes = noteGetter(lineItem.Properties);

        for(int i = 0; i < groupedImages.Count; i++){
            imagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[i], imagePaths);
        }

        return new(
            imagePaths: imagePaths,
            note: notes?[0].Value
        );
    }
}
