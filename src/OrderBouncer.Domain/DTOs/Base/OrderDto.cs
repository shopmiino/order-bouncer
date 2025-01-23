namespace OrderBouncer.Domain.DTOs.Base;

public record class OrderDto
{
    public string ShopifyOrderID;
    public ICollection<ProductDto>? Products;
    public string? Note;
    
    public OrderDto(string shopifyOrderId, ICollection<ProductDto>? products, string? note = null){
        ShopifyOrderID = shopifyOrderId;
        Products = products;
        Note = note;
    }
}
