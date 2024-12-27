using System;
using System.IO.Compression;

namespace OrderBouncer.Domain.Aggregates;

public class Order : Base
{
    public int Something {get; protected set;}
    protected Order(){
    }
    public Order(int Some):base(10){
        Something = Some;
    }

}
