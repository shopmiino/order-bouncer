using System;
using OrderBouncer.Domain.Entities.Base;
using SharedKernel.Constants;

namespace OrderBouncer.Domain.Entities;

public class NoteEntity : BaseEntity
{
    protected string? Note { get; private set; } = null;
    public bool Required { get; } = false;

    public NoteEntity(bool required = false)
    {
        Required = required;
    }

    protected internal bool HasNote()
    {
        if (Required) return true;

        return Note is not null;
    }

    protected internal void SetNote(string note)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(note.Length, Limits.MinCustomerNoteLength);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(note.Length, Limits.MaxCustomerNoteLength);

        Note = note;
    }

    protected internal void RemoveNote()
    {
        if (Required) throw new InvalidOperationException("Cannot remove required CustomerNote");

        Note = null;
    }
    protected internal string GetNote()
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(Note);

        return Note;
    }
}
