namespace OrderBouncer.Domain.DTOs.Base;

public record class OrderDto
{
    public ICollection<ProductDto>? Products;
    public string? Note;
    
    public OrderDto(ICollection<ProductDto>? products, string? note = null){
        Products = products;
        Note = note;
    }
}
