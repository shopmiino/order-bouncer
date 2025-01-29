using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Models;
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

    public RowElements GetElementsHasAtLeastOne(Dictionary<EntityTypeEnum, int> kvps)
    {
        var enums = kvps.Where(k => k.Value >= 1).Select(k => k.Key).ToArray();

        return new RowElements{Elements = enums, Count = 1};
    }

    public bool RemoveOneFromEach(Dictionary<EntityTypeEnum, int> kvps){
        bool changed = false;
        foreach(var kvp in kvps){
            if(kvp.Value < 1) continue;

            kvps[kvp.Key] -= 1;
            changed = true;
        }

        return changed;
    }

    public KeyValuePair<EntityTypeEnum, int>? GetHighestCountElement(Dictionary<EntityTypeEnum, int> kvps)
    {
        kvps.Where(k => k.Value >= 1).Max(a => a.Value);

        KeyValuePair<EntityTypeEnum, int>? kvp = null;

        try{
            kvp = kvps.Where(k => k.Value >= 1).MaxBy(a => a.Value);
        } catch (Exception ex){

        }

        return kvp;
    }
}
