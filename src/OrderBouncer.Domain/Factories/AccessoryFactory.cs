using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class AccessoryFactory : IFactory<AccessoryCreateDto, AccessoryEntity>
{
    public AccessoryEntity Create(AccessoryCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
