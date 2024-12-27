using System;
using System.Reflection.Metadata;
using SharedKernel;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public int OrderId {get; protected set;}
    public ProductTypeEnum Type { get; protected set; }
    public ICollection<AccessoryEntity>? Accessories {get; protected set;} = null;
    public ICollection<PetEntity>? Pets {get; protected set;} = null;

    public void AddPet(PetEntity pet){
        Pets ??= [];

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Pets.Count, Constants.MaxPetCountInProduct);

        PetEntity? exists = Pets.FirstOrDefault(p => pet.Id == p.Id); 
        exists ??= Pets.FirstOrDefault(p => p == pet);

        if(exists is not null){
            throw new InvalidOperationException("You cannot add already existing item to Pets list");
        }

        Pets.Add(pet);
    }

    public void RemovePet(PetEntity pet){
        if(Pets is null) throw new InvalidOperationException("Cannot remove from null list");

        PetEntity? exists = Pets.FirstOrDefault(p => pet.Id == p.Id); 
        exists ??= Pets.FirstOrDefault(p => p == pet);

        if(exists is null) {
            throw new InvalidOperationException("Pet must be exist in order to remove");
        }

        Pets.Remove(pet);
    }

    public void AddAccessory(AccessoryEntity accessory){
        Accessories ??= [];

        if(Type == ProductTypeEnum.PersonalizedSingle){
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Accessories.Count, Constants.MaxAccessoriesInStandard);
        } 

        if(Type == ProductTypeEnum.PersonalizedCouple){
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Accessories.Count, Constants.MaxAccessoriesInCouple);
        }

        AccessoryEntity? exists = Accessories.FirstOrDefault(p => accessory.Id == p.Id); 
        exists ??= Accessories.FirstOrDefault(p => p == accessory);

        if(exists is not null){
            throw new InvalidOperationException("You cannot add already existing item to Accessories list");
        }

        Accessories.Add(accessory);
    }

    public void RemoveAccessory(AccessoryEntity accessory){
        if(Accessories is null) throw new InvalidOperationException("Cannot remove from null list");

        AccessoryEntity? exists = Accessories.FirstOrDefault(p => accessory.Id == p.Id); 
        exists ??= Accessories.FirstOrDefault(p => p == accessory);

        if(exists is null) {
            throw new InvalidOperationException("Accessory must be exist in order to remove");
        }

        Accessories.Remove(accessory);
    }
}
