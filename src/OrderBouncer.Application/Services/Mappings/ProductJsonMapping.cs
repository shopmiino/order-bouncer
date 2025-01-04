using System;
using System.Text.Json.Nodes;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Mappings;

public class ProductJsonMapping : IJsonMapping<ProductEntity>
{
    private readonly IEntityFactory<ProductCreateDto, ProductEntity> _productFactory;
    private readonly IJsonMapping<AccessoryEntity> _accessoryMapping;
    private readonly IJsonMapping<FigureEntity> _figureMapping;
    private readonly IJsonMapping<PetEntity> _petMapping;
    private readonly IJsonExtractor _extractor;
    
    public ProductJsonMapping(IEntityFactory<ProductCreateDto, ProductEntity> productFactory, IJsonMapping<AccessoryEntity> accessoryMapping, IJsonMapping<FigureEntity> figureMapping, IJsonMapping<PetEntity> petMapping, IJsonExtractor extractor){
        _productFactory = productFactory;
        _accessoryMapping = accessoryMapping;
        _figureMapping = figureMapping;
        _petMapping = petMapping;
        _extractor = extractor;
    }

    public ProductEntity? Map(string json)
    {
        JsonNode? node = _extractor.Extract(json);
        
        ICollection<AccessoryEntity>? accessories = _accessoryMapping.MapMany(json);
        ICollection<FigureEntity>? figures = _figureMapping.MapMany(json);
        ICollection<PetEntity>? pets = _petMapping.MapMany(json);

        ProductEntity product = _productFactory.Create(new (accessories, pets, figures));
 
        return product;
    }

    public ICollection<ProductEntity>? MapMany(string json)
    {
        throw new NotImplementedException();
    }
}
