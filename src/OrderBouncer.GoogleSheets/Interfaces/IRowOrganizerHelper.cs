using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Models;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowOrganizerHelper
{
    public int GetCount<T>(ICollection<T>? coll);

    public Dictionary<EntityTypeEnum, int> GetElementCounts(OrderDto dto);
    public RowElements GetElementsHasAtLeastOne(Dictionary<EntityTypeEnum, int> kvps);
    public bool RemoveOneFromEach(Dictionary<EntityTypeEnum, int> kvps);
    public KeyValuePair<EntityTypeEnum,int>? GetHighestCountElement(Dictionary<EntityTypeEnum, int> kvps);
}
