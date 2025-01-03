using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class OrderCreateDto
{
    public ICollection<ProductEntity>? Products {get; protected set;}
    public OrderCreateDto(ICollection<ProductEntity>? products = null){
        Products = products;
    }
}
