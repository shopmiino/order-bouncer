namespace OrderBouncer.Domain.DTOs.Base;

public record class OrderDto
{
    public string ShopifyOrderID {get; init;}
    public ICollection<ProductDto>? Products {get; init;}
    public string? Note {get; init;}
    public DateTime? Date {get; init;}

    public Guid ScopeId {get; set;} 
    
    public OrderDto(){}
    public OrderDto(string shopifyOrderId, ICollection<ProductDto>? products, string? note = null, DateTime? date = null){
        ShopifyOrderID = shopifyOrderId;
        Products = products;
        Note = note;
        Date = date;
    }
}
