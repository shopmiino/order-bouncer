using System;
using OrderBouncer.Domain.DTOs.Base;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowOrganizerHelper
{
    public int GetCount<T>(ICollection<T>? coll);

    public Dictionary<EntityTypeEnum, int> GetElementCounts(OrderDto dto);
}
