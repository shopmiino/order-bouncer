using System;
using System.IO.Compression;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Aggregates;

public class Order : BaseAggregate
{
    public int Something { get; private set; }
    public ICollection<ProductEntity>? Products { get; private set; } = null;

    //productType
    //orderType
    protected Order()
    {
    }
    public Order(int Some) : base(10)
    {
        Something = Some;
    }


}
