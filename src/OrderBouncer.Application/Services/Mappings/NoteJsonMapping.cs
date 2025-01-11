using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class NoteJsonMapping : IJsonMapping<NoteEntity>
{
    private readonly IEntityFactory<NoteCreateDto, NoteEntity> _noteFactory;
    private readonly IJsonExtractor _extractor;

    public NoteJsonMapping(IEntityFactory<NoteCreateDto, NoteEntity> noteFactory, IJsonExtractor extractor){
        _noteFactory = noteFactory;
        _extractor = extractor;
    }

    public async Task<NoteEntity?> Map(string json)
    {
        JsonNode? node = await _extractor.Extract(json);

        string NoteText = string.Empty;

        NoteEntity note = _noteFactory.Create(new (NoteText));

        return note;
    }

    public Task<ICollection<NoteEntity>?> MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
