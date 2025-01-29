using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class RowDataService : IRowDataService
{
    public async Task<IList<FlattenRowDto>> Flatten(OrderDto dto)
    {
        string orderId = dto.ShopifyOrderID;
        string generalNore = dto.Note;
        dto.Products.First().
    }
}
