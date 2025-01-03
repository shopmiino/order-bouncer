using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class NoteFactory : IEntityFactory<NoteCreateDto, NoteEntity>
{
    public NoteEntity Create(NoteCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
