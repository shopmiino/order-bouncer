using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Mappings;

public class ImageJsonMapping : IJsonMapping<ImageEntity>
{
    private readonly IEntityFactory<ImageCreateDto, ImageEntity> _imageFactory;
    private readonly IJsonExtractor _extractor;
    public ImageJsonMapping(IEntityFactory<ImageCreateDto, ImageEntity> imageFactory, IJsonExtractor extractor){
        _imageFactory = imageFactory;
        _extractor =  extractor;
    }
    public ImageEntity? Map(string json)
    {
        JsonNode? node = _extractor.Extract(json);

        ImageTypeEnum imageType = ImageTypeEnum.Face;
        string filePath = string.Empty;

        ImageEntity image = _imageFactory.Create(new (imageType, filePath));
        return image;
    }

    public ICollection<ImageEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
