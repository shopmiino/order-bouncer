using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
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
    public async Task<AccessoryEntity?> Map(string json)
    {
        JsonNode? node = await _extractor.Extract(json);

        ICollection<ImageEntity>? images = await _imageMapping.MapMany(json);
        NoteEntity? note = await _noteMapping.Map(json);

        AccessoryEntity accessory = _accessoryFactory.Create(new (images, note));

        return accessory;
    }

    public Task<ICollection<AccessoryEntity>?> MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
