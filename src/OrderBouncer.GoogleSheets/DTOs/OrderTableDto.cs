using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.DTOs;

public record class OrderTableDto
{
    public ICollection<OrderRow> Rows { get; }
    public OrderTableDto(ICollection<OrderRow> rows)
    {
        Rows = rows;
    }
}
