namespace OrderBouncer.GoogleSheets.DTOs;

public record class OrderTableDto
{
    public ICollection<OrderRowDto> Rows {get;}
    public OrderTableDto(ICollection<OrderRowDto> rows){
        Rows = rows;
    }
}
