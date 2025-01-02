using System;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class ProductFactory : IProductFactory
{
    public ProductEntity Create()
    {
        throw new NotImplementedException();
    }
}
