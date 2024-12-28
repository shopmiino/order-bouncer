using System;
using OrderBouncer.Domain.Entities.Base;
using OrderBouncer.Domain.ValueObjects;
using OrderBouncer.Domain.ValueObjects.Sets;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class FigureEntity : NoteImageBaseEntity
{
    public FigureTypeEnum Type { get;}
    public AccessorySet AccessorySet { get; private set; }
    public Money FigureCost {get;}

    public Money AccessoryCost => AccessorySet.Accessories is null ? new Money(0,"TL") : new Money(AccessorySet.Accessories.Sum(a => a.Cost.Amount), "TL");
    public Money TotalCost => AccessoryCost + FigureCost;

    public FigureEntity(Money figureCost, FigureTypeEnum figureType, int parentId, EntityTypeEnum parentType) : base(parentId: parentId, parentType: parentType)
    {
        Type = figureType;
        AccessorySet = new(EntityTypeEnum.Figure);
        FigureCost = figureCost;
    }

    internal bool HasAccessory() => AccessorySet.HasAccessory();

    internal void AddAccessory(AccessoryEntity accessory) => AccessorySet.AddAccessory(accessory);

    internal void RemoveAccessory(AccessoryEntity accessory) => AccessorySet.RemoveAccessory(accessory);

}