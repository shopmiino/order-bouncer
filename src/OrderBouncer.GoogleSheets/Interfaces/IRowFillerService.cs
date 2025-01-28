using System;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowFillerService
{
    public OrderRow FillWithFlatten(FlattenRowDto dto, OrderRow baseRow);
}
