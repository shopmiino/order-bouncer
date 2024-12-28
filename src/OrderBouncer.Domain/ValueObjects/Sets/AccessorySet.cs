using System;
using OrderBouncer.Domain.Entities;
using SharedKernel.Constants;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.ValueObjects.Sets;

public class AccessorySet
{
    public ICollection<AccessoryEntity>? Accessories { get; private set; } = null;
    private EntityTypeEnum _parentType { get; set; }

    public AccessorySet(EntityTypeEnum parentType)
    {
        _parentType = parentType;
    }

    protected internal bool HasAccessory()
    {
        if (Accessories is null) return false;
        return Accessories.Count > 0;
    }

    protected internal void AddAccessory(AccessoryEntity accessory)
    {
        Accessories ??= [];

        if (_parentType == EntityTypeEnum.Figure)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Accessories.Count, Limits.MaxAccessoriesInFigure);
        }

        AccessoryEntity? exists = Accessories.FirstOrDefault(p => accessory.Id == p.Id);
        exists ??= Accessories.FirstOrDefault(p => p == accessory);

        if (exists is not null)
        {
            throw new InvalidOperationException("You cannot add already existing item to Accessories list");
        }

        Accessories.Add(accessory);
    }

    protected internal void RemoveAccessory(AccessoryEntity accessory)
    {
        if (Accessories is null) throw new InvalidOperationException("Cannot remove from null list");
        if (Accessories.Count <= 0) throw new InvalidOperationException("Accessories doesn't have any members in it. Attempting to remove from empty collection");
        
        AccessoryEntity? exists = Accessories.FirstOrDefault(p => accessory.Id == p.Id);
        exists ??= Accessories.FirstOrDefault(p => p == accessory);

        if (exists is null)
        {
            throw new InvalidOperationException("Accessory must be exist in order to remove");
        }

        Accessories.Remove(accessory);
    }
}
