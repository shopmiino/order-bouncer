using System;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IPetFactory
{
    public PetEntity Create();
}
