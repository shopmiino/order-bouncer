using System;
using System.Text.Json.Nodes;
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

    public AccessoryJsonMapping(IEntityFactory<AccessoryCreateDto, AccessoryEntity> accessoryFactory, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping){
        _accessoryFactory = accessoryFactory;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
    }
    public AccessoryEntity? Map(string json)
    {
        JsonNode? node = JsonNode.Parse(json);

        ICollection<ImageEntity>? images = _imageMapping.MapMany(json);
        NoteEntity? note = _noteMapping.Map(json);

        AccessoryEntity accessory = _accessoryFactory.Create(new (images, note));

        return accessory;
    }

    public ICollection<AccessoryEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
