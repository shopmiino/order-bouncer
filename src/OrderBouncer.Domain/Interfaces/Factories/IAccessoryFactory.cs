using System;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IAccessoryFactory
{
    public AccessoryEntity Create();
}
