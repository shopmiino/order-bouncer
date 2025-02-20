using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemExtrasConverterService : ILineItemExtrasConverterService
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemConverterHelperService _helper;
    public LineItemExtrasConverterService(ILineItemPropertyExtractor extractor, ILineItemConverterHelperService helper){
        _extractor = extractor;
        _helper = helper;
    }
    
    public async Task<BaseDto> ConvertExtra(LineItem lineItem, IList<NoteAttribute[]> props, Func<NoteAttribute[], NoteAttribute[]?> noteGetter, int position = 0, int notePosition = 0)
    {
        NoteAttribute[]? notes = noteGetter(lineItem.Properties);
        //NoteAttribute[]? notes = _extractor.GetPetNotes(lineItem.Properties);
        ICollection<string>? images = null;

        if (props[position] is not null || props[position].Length > 0)
        {
            images = await _helper.BatchImageSaveAndAdd(props[position], images ?? []);
            //startPos++;
        }

        return new(imagePaths: images, note: notes?[0].Value);
    }
}
