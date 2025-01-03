using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;
using SharedKernel.Enums;

namespace OrderBouncer.Application.Services.Mappings;

public class ImageJsonMapping : IJsonMapping<ImageEntity>
{
    private readonly IEntityFactory<ImageCreateDto, ImageEntity> _imageFactory;
    public ImageJsonMapping(IEntityFactory<ImageCreateDto, ImageEntity> imageFactory){
        _imageFactory = imageFactory;
    }
    public ImageEntity? Map(string json)
    {
        JsonNode? node = JsonNode.Parse(json);

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
