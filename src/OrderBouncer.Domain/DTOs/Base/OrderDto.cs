namespace OrderBouncer.Domain.DTOs.Base;

public record class OrderDto
{
    public string ShopifyOrderID;
    public ICollection<ProductDto>? Products;
    public string? Note;
    public DateTime? Date;
    
    public OrderDto(){}
    public OrderDto(string shopifyOrderId, ICollection<ProductDto>? products, string? note = null, DateTime? date = null){
        ShopifyOrderID = shopifyOrderId;
        Products = products;
        Note = note;
        Date = date;
    }
}
