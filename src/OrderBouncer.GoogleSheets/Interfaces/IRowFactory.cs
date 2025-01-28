using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowFactory
{
    public IRowFactory From(FlattenRowDto flattenRowDto);
    public OrderRowDto Build();
    
}
