using System;
using System.IO.Compression;
using OrderBouncer.Domain.Entities;
using SharedKernel.Enums;

namespace OrderBouncer.Domain.Aggregates;

public class Order : BaseAggregate
{
    public ICollection<ProductEntity>? Products { get; private set; } = null;
    protected Order()
    {
    }
    public Order() : base(10)
    {
        
    }

    public void AddProduct(ProductEntity product){
        product.ParentId = Id;
        product.ParentType = EntityTypeEnum.Order;
    }

    public void RemoveProduct(ProductEntity product){
        new FigureEntity();
    }

    public bool HasProduct(){
        if(Products is null) return false;
        return Products.Count > 0;
    }

}
