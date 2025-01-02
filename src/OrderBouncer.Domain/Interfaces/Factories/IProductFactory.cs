using System;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IProductFactory
{
    public ProductEntity Create();
}
