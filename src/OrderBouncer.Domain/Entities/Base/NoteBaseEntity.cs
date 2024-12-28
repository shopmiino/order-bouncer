using System;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities.Base;

public abstract class NoteBaseEntity : BaseEntity
{
    public NoteEntity CustomerNote { get; } = new();

    protected NoteBaseEntity() { }
    protected NoteBaseEntity(int? id = null, int? parentId = null, EntityTypeEnum? parentType = null, bool isNoteRequired = false) : base(id, parentId, parentType)
    {
        if (isNoteRequired) CustomerNote = new(true);
    }

    internal bool HasNote() => CustomerNote.HasNote();

    internal void SetNote(string note) => CustomerNote.SetNote(note);

    internal void RemoveNote() => CustomerNote.RemoveNote();

    internal string GetNote() => CustomerNote.GetNote();

}
