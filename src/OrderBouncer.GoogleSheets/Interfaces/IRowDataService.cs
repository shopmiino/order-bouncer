using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowDataService
{
    public FlattenRowDto Flatten(OrderDto dto);
    public OrderRowDto CreateRowFromFlatten(FlattenRowDto dto);
}
