using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Services;

public class RowConverterService : IRowConverterService
{
    private readonly IRowFillerService _filler;
    public RowConverterService(IRowFillerService filler){
        _filler = filler;
    }
    public ICollection<FlattenRowDto> ConvertToFlatten(Stack<RowElements> elements, OrderDto orderDto)
    {
        string code = orderDto.ShopifyOrderID;
        DateTime date = orderDto.Date ?? DateTime.Now;
        
        ICollection<FlattenRowDto> flattens = [];

        for(int i = 0; i < elements.Count; i++){
            var element = elements.Pop();
            var flatten = _filler.FillFlattenWithElements(element, new(code, date));

            flattens.Add(flatten);
        }
        return flattens;
    }
}
