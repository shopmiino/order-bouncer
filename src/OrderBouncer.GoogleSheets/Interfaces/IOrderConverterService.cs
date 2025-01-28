using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IOrderConverterService
{
    public Task<OrderTableDto> ConvertToTable(OrderDto dto);
    public Task<ICollection<OrderRow>> ConvertToRowCollection(OrderDto dto);
}
