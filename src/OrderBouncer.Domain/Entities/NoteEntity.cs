using System;
using OrderBouncer.Domain.Entities.Base;
using SharedKernel.Constants;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class NoteEntity : BaseEntity
{
    protected string? Note { get; private set; } = null;
    public bool Required { get; } = false;
    public bool IsAttached { get; private set; } = false;

    public NoteEntity(bool required = false, int? parentId = null, EntityTypeEnum? parentType = null) : base(parentId: parentId, parentType: parentType)
    {
        Required = required;
    }

    protected internal void Attach(int parentId, EntityTypeEnum parentType)
    {
        ParentId = parentId;
        ParentType = parentType;

        IsAttached = true;
    }

    protected internal bool HasNote()
    {
        if (Required) return true;

        return Note is not null;
    }

    protected internal void SetNote(string note)
    {
        if (!IsAttached) throw new InvalidOperationException("Can't set value on unattached note");

        ArgumentOutOfRangeException.ThrowIfLessThan(note.Length, Limits.MinCustomerNoteLength);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(note.Length, Limits.MaxCustomerNoteLength);

        Note = note;
    }

    protected internal void RemoveNote()
    {
        if (!IsAttached) throw new InvalidOperationException("Can't remove unattached note");

        if (Required) throw new InvalidOperationException("Cannot remove required CustomerNote");

        Note = null;
    }
    protected internal string GetNote()
    {
        if (!IsAttached) throw new InvalidOperationException("Can't get unattached note");

        ArgumentNullException.ThrowIfNullOrWhiteSpace(Note);

        return Note;
    }
}
