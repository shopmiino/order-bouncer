using System;
using OrderBouncer.Domain.ValueObjects.Sets;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities.Base;

public abstract class NoteImageBaseEntity : NoteBaseEntity
{
    public ImageSet ImageSet { get; } = new();

    protected NoteImageBaseEntity() { }
    protected NoteImageBaseEntity(int? id = null, int? parentId = null, EntityTypeEnum? parentType = null, bool isNoteRequired = false, bool isImageRequired = false) : base(id, parentId, parentType, isNoteRequired)
    {
        if (isImageRequired) ImageSet = new(true);
    }

    internal bool HasImage() => ImageSet.HasImage();

    internal void AddImage(ImageEntity image) => ImageSet.AddImage(image);

    internal void RemoveImage(ImageEntity image) => ImageSet.RemoveImage(image);

}
