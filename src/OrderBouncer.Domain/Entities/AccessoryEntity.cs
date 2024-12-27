using System;
using System.ComponentModel.DataAnnotations;
using OrderBouncer.Domain.ValueObjects;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class AccessoryEntity: BaseEntity
{
    public int ParentId {get; protected set;}
    public EntityTypeEnum ParentType {get; protected set;}
    public ICollection<ImageEntity>? Images {get; protected set;} = null;
    public string CustomerNote {get; protected set;}
    public Money Cost {get; private set;}
    public AccessoryEntity(int parentId, EntityTypeEnum parentType, Money cost, string customerNote){
        
        ArgumentOutOfRangeException.ThrowIfNegative(parentId);

        ArgumentOutOfRangeException.ThrowIfLessThan(customerNote.Length, 20);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(customerNote.Length, 2000);

        ParentId = parentId;
        Cost = cost;
        CustomerNote = customerNote;
        ParentType = parentType;
    }
}
