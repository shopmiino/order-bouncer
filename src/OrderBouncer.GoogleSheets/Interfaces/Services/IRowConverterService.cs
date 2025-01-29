using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Interfaces.Services;

public interface IRowConverterService
{
    public ICollection<FlattenRowDto> ConvertToFlatten(Stack<RowElements> elements, OrderDto orderDto);
}
