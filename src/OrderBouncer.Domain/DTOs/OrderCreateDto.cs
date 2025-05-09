using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class OrderCreateDto
{
    public List<ProductEntity>? Products {get; protected set;}
    public OrderCreateDto(List<ProductEntity>? products = null){
        Products = products;
    }
}
