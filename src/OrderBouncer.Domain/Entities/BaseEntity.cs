using System;

namespace OrderBouncer.Domain.Entities;

public class BaseEntity
{
    public int Id {get; protected set;}
    public DateTime CreatedAt {get; protected set;} = DateTime.UtcNow;
    public DateTime? DeletedAt {get; protected set;} = null;

    protected BaseEntity(){}

    protected BaseEntity(int id){
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("Id couldn't be less than or equal to a zero");
        }

        Id = id;
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
