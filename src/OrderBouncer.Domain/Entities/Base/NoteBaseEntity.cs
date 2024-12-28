using System;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities.Base;

public abstract class NoteBaseEntity : BaseEntity
{
    public NoteEntity Note { get; } = new();

    protected NoteBaseEntity() { }
    protected NoteBaseEntity(int? id = null, int? parentId = null, EntityTypeEnum? parentType = null, bool isNoteRequired = false) : base(id, parentId, parentType)
    {
        if (isNoteRequired) Note = new(true, parentId, parentType);
    }

    internal bool HasNote() => Note.HasNote();

    internal void AttachNote(int parentId, EntityTypeEnum parentType) => Note.Attach(parentId, parentType);

    internal void SetNote(string note) => Note.SetNote(note);

    internal void RemoveNote() => Note.RemoveNote();

    internal string GetNote() => Note.GetNote();

}
