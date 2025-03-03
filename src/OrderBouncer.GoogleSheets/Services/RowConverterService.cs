using System;
using System.Reflection;
using Google.Apis.Sheets.v4.Data;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Services;

public class RowConverterService : IRowConverterService
{
    private readonly IRowFillerService _filler;
    private readonly IRowConverterHelperService _helper;
    public RowConverterService(IRowFillerService filler, IRowConverterHelperService helper){
        _filler = filler;
        _helper = helper;
    }

    public IList<CellData> ConvertToCellDatas(OrderRow row)
    {
        List<CellData> cellDatas = [];
        
        foreach(Cell cell in row.GetCellsInOrder()){
            cellDatas.Add(_helper.CellToSpreadSheetCell(cell));
        }
        
        return cellDatas;
    }

    public ICollection<FlattenRowDto> ConvertToFlatten(Stack<RowElements> elements, OrderDto orderDto)
    {
        string code = orderDto.ShopifyOrderID;
        DateTime date = orderDto.Date ?? DateTime.Now;
        
        ICollection<FlattenRowDto> flattens = [];
        int elementCount = elements.Count;

        IList<string?>? names = orderDto.Products?.First().Figures?.Select(f => f.Name).ToList();

        for(int i = 0; i < elementCount; i++){
            var element = elements.Pop();
            var flatten = _filler.FillFlattenWithElements(element, new(code, date, names?[i]));

            flattens.Add(flatten);
        }
        return flattens;
    }
}
