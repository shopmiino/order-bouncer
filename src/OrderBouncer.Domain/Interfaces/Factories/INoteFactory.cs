using System;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface INoteFactory
{
    public NoteEntity Create();
}
