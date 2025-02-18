using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class PetFactory : IFactory<PetCreateDto, PetEntity>
{
    public PetEntity Create(PetCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
