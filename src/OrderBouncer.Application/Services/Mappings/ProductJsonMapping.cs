using System;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class ProductJsonMapping : IJsonMapping<ProductEntity>
{
    private readonly IProductFactory _productFactory;
    private readonly IJsonMapping<AccessoryEntity> _accessoryMapping;
    private readonly IJsonMapping<FigureEntity> _figureMapping;
    private readonly IJsonMapping<PetEntity> _petMapping;

    public ProductJsonMapping(IProductFactory productFactory, IJsonMapping<AccessoryEntity> accessoryMapping, IJsonMapping<FigureEntity> figureMapping, IJsonMapping<PetEntity> petMapping){
        
    }

    public ProductEntity Map(string json)
    {
        throw new NotImplementedException();
    }

    public ProductEntity[] MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
