using System;
using OrderBouncer.GoogleSheets.DTOs;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowConverterService
{
    public OrderRowDto ConvertFromFlatten(FlattenRowDto dto);
}
