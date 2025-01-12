using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Services.Extractors.Profiles;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class AccessoryJsonMapping : IJsonMapping<AccessoryEntity>
{
    private readonly IEntityFactory<AccessoryCreateDto, AccessoryEntity> _accessoryFactory;
    private readonly IJsonMapping<ImageEntity> _imageMapping;
    private readonly IJsonMapping<NoteEntity> _noteMapping;
    private readonly IJsonExtractor _extractor;

    public AccessoryJsonMapping(IEntityFactory<AccessoryCreateDto, AccessoryEntity> accessoryFactory, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping, IJsonExtractor extractor){
        _accessoryFactory = accessoryFactory;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
        _extractor = extractor;
    }
    public async Task<AccessoryEntity?> Map(JsonNode json)
    {
        JsonNode? node = await _extractor.Extract<AccessoryExtractorProfile>(json);

        ICollection<ImageEntity>? images = await _imageMapping.MapMany(node);
        NoteEntity? note = await _noteMapping.Map(node);

        AccessoryEntity accessory = _accessoryFactory.Create(new (images, note));

        return accessory;
    }

    public async Task<ICollection<AccessoryEntity>?> MapMany(JsonNode json)
    {
        JsonNode? node = await _extractor.Extract<AccessoryExtractorProfile>(json);
        JsonArray? properties = node["properties"].AsArray();
        JsonNode[]? accessories = properties.Where(prop => prop.GetPropertyName() == "Accessory").ToArray();

        throw new NotImplementedException();
    }
}
