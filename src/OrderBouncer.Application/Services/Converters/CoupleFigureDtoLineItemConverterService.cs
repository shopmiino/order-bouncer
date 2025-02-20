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

    public Task<(FigureDto[], ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem)
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

        ICollection<string> firstImagePaths = [];
        ICollection<string> secondImagePaths = [];

        AccessoryDto? firstAccessoryDto = null;
        AccessoryDto? secondAccessoryDto = null;

        PetDto? petDto = null;

        NoteAttribute[]? figureNotes = _extractor.GetFigureNotes(lineItem.Properties);
        NoteAttribute[]? nameNotes = _extractor.GetNameNotes(lineItem.Properties);

    }
}
