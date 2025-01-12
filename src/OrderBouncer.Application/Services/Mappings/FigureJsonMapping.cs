using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Services.Extractors.Profiles;
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

    public async Task<FigureEntity?> Map(JsonNode json)
    {
        JsonNode? node = await _extractor.Extract<FigureExtractorProfile>(json);

        ICollection<AccessoryEntity>? accessories = await _accessoryMapping.MapMany(node);
        ICollection<ImageEntity>? images = await _imageMapping.MapMany(node);
        NoteEntity? note = await _noteMapping.Map(node);

        FigureEntity figure = _figureFactory.Create(new (accessories, images, note));
        return figure;
    }

    public Task<ICollection<FigureEntity>?> MapMany(JsonNode json)
    {
        throw new NotImplementedException();
    }
}
