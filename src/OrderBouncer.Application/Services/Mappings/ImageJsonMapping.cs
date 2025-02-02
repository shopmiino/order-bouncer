using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Services.Extractors.Profiles;
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
    public async Task<ImageEntity?> Map(JsonNode json)
    {
        JsonNode? node = await _extractor.Extract<ImageExtractorProfile>(json);

        ImageTypeEnum imageType = node[""].GetValue<ImageTypeEnum>();
        string filePath = node[""].ToString();

        ImageEntity image = _imageFactory.Create(new (imageType, filePath));
        return image;
    }

    public Task<ICollection<ImageEntity>?> MapMany(JsonNode json)
    {
        throw new NotImplementedException();
    }
}
