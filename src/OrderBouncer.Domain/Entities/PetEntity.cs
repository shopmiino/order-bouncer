using System;
using OrderBouncer.Domain.Entities.Base;
using OrderBouncer.Domain.ValueObjects;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class PetEntity : NoteImageBaseEntity
{
    public Money Cost { get;}
    public PetEntity(Money cost, int parentId, EntityTypeEnum parentType):base(isImageRequired: true, parentId: parentId, parentType: parentType)
    {
        Cost = cost;
    }

}
