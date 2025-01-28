using System;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowConverterService : IRowConverterService
{
    public OrderRowDto ConvertFromFlatten(FlattenRowDto dto)
    {
        new OrderRowDto()
    }
}
