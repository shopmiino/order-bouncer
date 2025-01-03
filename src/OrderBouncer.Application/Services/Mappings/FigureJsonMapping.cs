using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class FigureJsonMapping : IJsonMapping<FigureEntity>
{
    private readonly IEntityFactory<FigureCreateDto, FigureEntity> _figureFactory;
    private readonly IJsonMapping<AccessoryEntity> _accessoryMapping;
    private readonly IJsonMapping<ImageEntity> _imageMapping;
    private readonly IJsonMapping<NoteEntity> _noteMapping;

    public FigureJsonMapping(IEntityFactory<FigureCreateDto, FigureEntity> figureFactory, IJsonMapping<AccessoryEntity> accessoryMapping, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping){
        _figureFactory = figureFactory;
        _accessoryMapping = accessoryMapping;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
    }

    public FigureEntity? Map(string json)
    {
        JsonNode? node = JsonNode.Parse(json);

        ICollection<AccessoryEntity>? accessories = _accessoryMapping.MapMany(json);
        ICollection<ImageEntity>? images = _imageMapping.MapMany(json);
        NoteEntity? note = _noteMapping.Map(json);

        FigureEntity figure = _figureFactory.Create(new (accessories, images, note));
        return figure;
    }

    public ICollection<FigureEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
