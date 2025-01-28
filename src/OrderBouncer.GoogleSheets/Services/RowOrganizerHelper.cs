using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Services;

public class RowOrganizerHelper : IRowOrganizerHelper
{
    public int GetCount<T>(ICollection<T>? coll)
    {
        return coll is null ? 0 : coll.Count;
    }

    public Dictionary<EntityTypeEnum, int> GetElementCounts(OrderDto dto)
    {
        if (dto.Products is null) throw new ArgumentNullException("Order has no product in it");

        Dictionary<EntityTypeEnum, int> keyValuePairs = [];

        ProductDto pDto = dto.Products.First();

        keyValuePairs.Add(EntityTypeEnum.Accessory, GetCount(pDto.Accessories));
        keyValuePairs.Add(EntityTypeEnum.Pet, GetCount(pDto.Pets));
        keyValuePairs.Add(EntityTypeEnum.Keychain, GetCount(pDto.Keychains));
        keyValuePairs.Add(EntityTypeEnum.Figure, GetCount(pDto.Figures));

        return keyValuePairs;
    }
}
