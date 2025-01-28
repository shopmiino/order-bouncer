using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class OrderConverterService : IOrderConverterService
{
    public Task<ICollection<OrderRow>> ConvertToRowCollection(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<OrderTableDto> ConvertToTable(OrderDto dto)
    {
        throw new NotImplementedException();
    }
}
