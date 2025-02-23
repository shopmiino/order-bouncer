using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemExtrasConverterService : ILineItemExtrasConverterService
{
    private readonly ILineItemConverterHelperService _helper;
    private readonly ILogger<LineItemExtrasConverterService> _logger;
    public LineItemExtrasConverterService(ILineItemConverterHelperService helper, ILogger<LineItemExtrasConverterService> logger){
        _helper = helper;
        _logger = logger;
    }
    
    public async Task<BaseDto> ConvertExtra(Guid scopeId, LineItem lineItem, IList<NoteAttribute[]> props, Func<NoteAttribute[], NoteAttribute[]?> noteGetter, int position = 0, int notePosition = 0, bool hasNoImage = false)
    {
        _logger.LogInformation("ConvertExtra is starting with propArrayCount: {0}, position: {1}, notePosition: {1}", props.Count(), position, notePosition);
        NoteAttribute[]? notes = noteGetter(lineItem.Properties);
        //NoteAttribute[]? notes = _extractor.GetPetNotes(lineItem.Properties);
        ICollection<string>? images = null;
        _logger.LogDebug("imagePaths initialized to null");

        if(hasNoImage) goto NoImage;

        if (props[position] is not null && props[position].Count() > 0)
        {
            _logger.LogDebug("props[{0}] is not null and props[{1}].Count() (count: {2}) > 0", position, position, props[position].Count());
            images = await _helper.BatchImageSaveAndAdd(props[position], images ?? [], scopeId);
            _logger.LogDebug("Returned imagePaths's count is: {0}", images.Count());
            //startPos++;
        } else {
            _logger.LogWarning("props[{0}] is null or props[{1}].Count() (count: {2}) <= 0", position, position, props[position].Count());
        }

        NoImage:

        string? noteString = notes?[0].Value;
        _logger.LogDebug("Note is {0}", noteString);

        return new(imagePaths: images, note: noteString);
    }
}
