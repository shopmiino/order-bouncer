using System;
using System.ComponentModel.DataAnnotations;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities.Base;

public abstract class BaseEntity
{
    [Key]
    public int Id {get; protected set;}
    public int ParentId {get; protected set;}
    public EntityTypeEnum ParentType {get; protected set;}
    public DateTime CreatedAt {get; protected set;} = DateTime.UtcNow;
    public DateTime? DeletedAt {get; protected set;} = null;

    protected BaseEntity(){}

    protected BaseEntity(int? id = null, int? parentId = null, EntityTypeEnum? parentType = null){
        if(id is not null){
            ArgumentOutOfRangeException.ThrowIfLessThan(id.Value, 0);
            Id = id.Value;
        }

        if(parentId is not null){
            ArgumentOutOfRangeException.ThrowIfLessThan(parentId.Value, 0);
            ParentId = parentId.Value;
        }

        if(parentType is not null){
            ParentType = parentType.Value;
        }
    }
    protected void MarkAsDeleted()
    {
        if (DeletedAt is not null)
        {
            throw new InvalidOperationException("Cannot mark previously deleted entity as deleted again!");
        }

        DeletedAt = DateTime.UtcNow;
    }

    protected void RestoreDeleted()
    {
        if (DeletedAt is null)
        {
            throw new InvalidOperationException("Can't restore an entity which isn't deleted");
        }

        DeletedAt = null;
    }

}
