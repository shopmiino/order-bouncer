using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class ProductFactory : IFactory<ProductCreateDto, ProductEntity>
{
    public ProductEntity Create(ProductCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
