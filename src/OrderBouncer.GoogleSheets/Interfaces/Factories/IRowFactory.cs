using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces.Factories;

public interface IRowFactory
{
    public IRowFactory Create();
    public IRowFactory From(FlattenRowDto flattenRowDto);
    public OrderRow Build();

}
