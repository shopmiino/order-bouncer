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

    public void AddProduct(ProductEntity product){
        product.ParentId = Id;
        product.ParentType = EntityTypeEnum.Order;
    }

    public void RemoveProduct(ProductEntity product){
        
    }

    public bool HasProduct(){
        if(Products is null) return false;
        return Products.Count > 0;
    }

}