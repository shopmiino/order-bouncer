using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
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
    private readonly IJsonExtractor _extractor;

    public FigureJsonMapping(IEntityFactory<FigureCreateDto, FigureEntity> figureFactory, IJsonMapping<AccessoryEntity> accessoryMapping, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping, IJsonExtractor extractor){
        _figureFactory = figureFactory;
        _accessoryMapping = accessoryMapping;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
        _extractor = extractor;
    }

    public FigureEntity? Map(string json)
    {
        JsonNode? node = _extractor.Extract(json);

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
