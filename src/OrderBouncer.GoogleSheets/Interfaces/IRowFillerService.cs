using System;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowFillerService
{
    public OrderRow FillWithFlatten(FlattenRowDto dto, OrderRow baseRow);
    public FlattenRowDto FillFlattenWithElements(RowElements elements, FlattenRowDto flatten);
}
