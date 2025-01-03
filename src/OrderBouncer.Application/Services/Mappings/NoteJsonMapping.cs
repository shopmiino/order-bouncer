using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class NoteJsonMapping : IJsonMapping<NoteEntity>
{
    private readonly IEntityFactory<NoteCreateDto, NoteEntity> _noteFactory;

    public NoteJsonMapping(IEntityFactory<NoteCreateDto, NoteEntity> noteFactory){
        _noteFactory = noteFactory;
    }

    public NoteEntity? Map(string json)
    {
        JsonNode? node = JsonNode.Parse(json);

        string NoteText = string.Empty;

        NoteEntity note = _noteFactory.Create(new (NoteText));

        return note;
    }

    public ICollection<NoteEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
