using System;

namespace OrderBouncer.Application.Options;

public class ShopifySettings
{
    public ProductTableItem[] ProductIdTable {get; set;} = [];
}

public class ProductTableItem{
    public long ShopifyID {get; set;}
    public int InternalID {get; set;}
    public string Title {get; set;} = string.Empty;
}
