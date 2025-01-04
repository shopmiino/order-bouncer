using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class PetJsonMapping : IJsonMapping<PetEntity>
{
    private readonly IEntityFactory<PetCreateDto, PetEntity> _petFactory;
    private readonly IJsonMapping<ImageEntity> _imageMapping;
    private readonly IJsonMapping<NoteEntity> _noteMapping;
    private readonly IJsonExtractor _extractor;

    public PetJsonMapping(IEntityFactory<PetCreateDto, PetEntity> petFactory, IJsonMapping<ImageEntity> imageMapping, IJsonMapping<NoteEntity> noteMapping, IJsonExtractor extractor){
        _petFactory = petFactory;
        _imageMapping = imageMapping;
        _noteMapping = noteMapping;
        _extractor = extractor;
    }

    public PetEntity? Map(string json)
    {
        JsonNode? node = _extractor.Extract(json);

        ICollection<ImageEntity>? images = _imageMapping.MapMany(json);
        NoteEntity? note = _noteMapping.Map(json);

        PetEntity pet = _petFactory.Create(new (images, note));
        return pet;
    }

    public ICollection<PetEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
