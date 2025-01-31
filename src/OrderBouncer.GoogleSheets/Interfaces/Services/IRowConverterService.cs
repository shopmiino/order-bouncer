using System;
using Google.Apis.Sheets.v4.Data;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Interfaces.Services;

public interface IRowConverterService
{
    public ICollection<FlattenRowDto> ConvertToFlatten(Stack<RowElements> elements, OrderDto orderDto);
    public IList<CellData> ConvertToCellDatas(OrderRow row);
}
