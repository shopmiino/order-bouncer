using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowDataService
{
    public Task<IList<FlattenRowDto>> Flatten(OrderDto dto);
}
