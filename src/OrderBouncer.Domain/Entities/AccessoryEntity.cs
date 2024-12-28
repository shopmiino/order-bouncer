using System;
using OrderBouncer.Domain.Entities.Base;
using OrderBouncer.Domain.ValueObjects;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class AccessoryEntity: NoteBaseEntity
{
    public Money Cost {get;}
    public AccessoryEntity(int parentId, EntityTypeEnum parentType, Money cost):base(parentId:parentId, parentType: parentType){
        

        Cost = cost;

    }
}
