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
    public SingleFigureDtoLineItemConverterService(ILineItemPropertyExtractor extractor, IImageSaverService imageSaver)
    {
        _extractor = extractor;
        _imageSaver = imageSaver;
    }

    public async Task<FigureDto> Convert(LineItem lineItem)
    {
        SingleFigureVariant variant = VariantMappings.SingleFigureVariantMappings[lineItem.VariantId];

        var groupedImages = _extractor.GroupImages(lineItem.Properties);
        ICollection<string> imagePaths = [];
        AccessoryDto? accessoryDto = null;

        if (variant.HasExtraAccessory)
        {
            ICollection<string>? accessoryImages = [];

            foreach (var item in groupedImages[0])
            {
                string path = await _imageSaver.Save(item.Value, item.Name);
                accessoryImages.Add(path);
            }

            accessoryDto = new(imagePaths: accessoryImages);
        }

        FigureDto? figureDto = null;

        if (accessoryDto is not null)
        {
            figureDto = new(accessoryDtos: [accessoryDto], imagePaths: imagePaths);
        }
        else
        {
            figureDto = new(imagePaths: imagePaths);
        }

        return figureDto;
    }
}
