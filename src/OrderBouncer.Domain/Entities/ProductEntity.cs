using System;
using OrderBouncer.Domain.Entities.Base;
using OrderBouncer.Domain.ValueObjects;
using OrderBouncer.Domain.ValueObjects.Sets;
using SharedKernel.Constants;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class ProductEntity : NoteImageBaseEntity
{
    public ProductTypeEnum Type { get; protected set; }
    public AccessorySet AccessorySet { get; private set; }
    public PetSet PetSet {get; private set;}
    public FigureSet FigureSet {get; private set;}

    public Money PetsCost => PetSet.Pets is null ? new(0,"TL") : new(PetSet.Pets.Sum(p => p.Cost.Amount),"TL");
    public Money AccessoriesCost => AccessorySet.Accessories is null ? new(0,"TL") : new(AccessorySet.Accessories.Sum(p => p.Cost.Amount),"TL");
    public Money FiguresCost => FigureSet.Figures is null ? new(0,"TL") : new(FigureSet.Figures.Sum(p => p.TotalCost.Amount),"TL");
    public Money TotalCost => AccessoriesCost + FiguresCost + PetsCost; 

    public ProductEntity(ProductTypeEnum productType, int parentId, EntityTypeEnum parentType) : base(parentId: parentId, parentType: parentType)
    {
        Type = productType;
        AccessorySet = new(EntityTypeEnum.Product);
        PetSet = new();
        FigureSet = new(productType);
    }

    internal bool HasAccessory() => AccessorySet.HasAccessory();
    internal void AddAccessory(AccessoryEntity accessory) => AccessorySet.AddAccessory(accessory);
    internal void RemoveAccessory(AccessoryEntity accessory) => AccessorySet.RemoveAccessory(accessory);

    internal bool HasPet() => PetSet.HasPet();
    internal void AddPet(PetEntity pet) => PetSet.AddPet(pet);
    internal void RemovePet(PetEntity pet) => PetSet.RemovePet(pet);

    internal bool HasFigure() => FigureSet.HasFigure();
    internal void AddFigure(FigureEntity figure) => FigureSet.AddFigure(figure);
    internal void RemoveFigure(FigureEntity figure) => FigureSet.RemoveFigure(figure);

}
