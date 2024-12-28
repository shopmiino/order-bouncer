using System;
using OrderBouncer.Domain.Entities;
using SharedKernel.Constants;

namespace OrderBouncer.Domain.ValueObjects.Sets;

public class PetSet
{
    public ICollection<PetEntity>? Pets {get; set;} = null;
    
    public PetSet(){

    }

    protected internal bool HasPet()
    {
        if (Pets is null) return false;
        return Pets.Count > 0;
    }

    protected internal void AddPet(PetEntity pet)
    {
        Pets ??= [];

        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(Pets.Count, Limits.MaxPetCountInProduct);

        PetEntity? exists = Pets.FirstOrDefault(p => pet.Id == p.Id);
        exists ??= Pets.FirstOrDefault(p => p == pet);

        if (exists is not null)
        {
            throw new InvalidOperationException("You cannot add already existing item to Pets list");
        }

        Pets.Add(pet);
    }

    protected internal void RemovePet(PetEntity pet)
    {
        if (Pets is null) throw new InvalidOperationException("Cannot remove from null list");
        if (Pets.Count <= 0) throw new InvalidOperationException("Pets doesn't have any members in it. Attempting to remove from empty collection");
        
        PetEntity? exists = Pets.FirstOrDefault(p => pet.Id == p.Id);
        exists ??= Pets.FirstOrDefault(p => p == pet);

        if (exists is null)
        {
            throw new InvalidOperationException("Pet must be exist in order to remove");
        }

        Pets.Remove(pet);
    }
}
