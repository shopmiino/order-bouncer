using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class BaseDtoLineItemConverterService : ILineItemsBaseConverterService
{
    private readonly ILineItemPropertyExtractor _extractor;
    private readonly ILineItemConverterHelperService _helper;
    private readonly ILogger<BaseDtoLineItemConverterService> _logger;

    public BaseDtoLineItemConverterService(ILineItemPropertyExtractor extractor, ILineItemConverterHelperService helper, ILogger<BaseDtoLineItemConverterService> logger){
        _extractor = extractor;
        _helper = helper;
        _logger = logger;
    }

    public async Task<BaseDto> GenericConvert(LineItem lineItem, Func<NoteAttribute[], NoteAttribute[]?> noteGetter, Guid scopeId)
    {
        IList<NoteAttribute[]>? groupedImages = null;
        try{
            groupedImages = _extractor.GroupImages(lineItem.Properties);
        } catch (Exception ex) {
            _logger.LogError(ex, "Error while grouping images");
        }

        if(groupedImages is null){
            throw new ArgumentNullException("No Grouped Images here, element is null");
        }
        if(groupedImages.Count <= 0){
            throw new ArgumentNullException("No Grouped Images here, element is empty");
        }

        ICollection<string> imagePaths = [];
        _logger.LogDebug("ImagePath collection initialized to empty");

        NoteAttribute[]? notes = null;
        try{
            notes = noteGetter(lineItem.Properties);
            _logger.LogDebug("Notes got from selected extractor. Total of {0}", notes?.Count());
        } catch (Exception ex){
            _logger.LogError(ex, "Error while getting NOTES");
        }

        _logger.LogDebug("{0} iterations are starting for figure images. First iteration for head images, second iteration for body images", groupedImages.Count());

        try{
            for(int i = 0; i < groupedImages.Count(); i++){
                imagePaths = await _helper.BatchImageSaveAndAdd(groupedImages[i], imagePaths, scopeId);
            }
        } catch (Exception ex) {
            _logger.LogError(ex, "Error while iterating FIGURE'S IMAGES");
        }

        return new(
            imagePaths: imagePaths,
            note: notes?[0].Value
        );
    }
}
