namespace OrderBouncer.Domain.DTOs.Base;

public record class OrderDto
{
    public ICollection<FigureDto> Figures;
    public ICollection<AccessoryDto> Accessories;
    
    public OrderDto(){

    }
}
