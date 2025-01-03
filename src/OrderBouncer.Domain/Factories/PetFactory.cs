using System;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class PetFactory : IPetFactory
{
    public PetEntity Create()
    {
        throw new NotImplementedException();
    }
}
