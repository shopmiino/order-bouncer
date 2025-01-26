using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IOrderConverterService
{
    public Task<OrderTableDto> ConvertToTable(OrderDto dto);
    public Task<ICollection<OrderRowDto>> ConvertToRowCollection(OrderDto dto);
}
