using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.DTOs;

public record class OrderCreateDto
{
    public ProductEntity[]? Products {get; protected set;}
    public OrderCreateDto(ProductEntity[]? products = null){
        Products = products;
    }
}
