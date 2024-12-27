using System;
using System.ComponentModel.DataAnnotations;

namespace OrderBouncer.Domain.Aggregates;

public abstract class BaseAggregate
{
    [Key]
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; } = null;
    public int Version { get; private set; } = 1;

    protected BaseAggregate()
    {
    }

    protected BaseAggregate(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException("Id couldn't be less than or equal to a zero");
        }

        Id = id;
    }

    protected void MarkAsModified()
    {
        if (DeletedAt is not null)
        {
            throw new InvalidOperationException("Cannot modify a deleted entity.");
        }

        if (ModifiedAt < CreatedAt)
        {
            throw new InvalidOperationException("ModifiedAt cannot be earlier than CreatedAt.");
        }

        ModifiedAt = DateTime.UtcNow;
        Version++;
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
