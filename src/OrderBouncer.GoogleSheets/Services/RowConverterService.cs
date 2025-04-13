using System;
using System.Reflection;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RowConverterService> _logger;
    public RowConverterService(IRowFillerService filler, IRowConverterHelperService helper, ILogger<RowConverterService> logger){
        _filler = filler;
        _helper = helper;
        _logger = logger;
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
        _logger.LogInformation("ConvertToFlatten is started with {0} elements", elements.Count);

        string code = orderDto.ShopifyOrderID;
        DateTime date = orderDto.Date ?? DateTime.Now;
        
        ICollection<FlattenRowDto> flattens = [];
        int elementCount = elements.Count;

        IEnumerable<string>? tempNames = orderDto.Products?.First().Figures?.Select(f => f.Name ?? string.Empty);
        IList<string>? names = tempNames is null ? null : [.. tempNames];
        _logger.LogDebug("Names collection is generated with {0} elements", names?.Count);

        int nameIteration = 0;

        for(int i = 0; i < elementCount; i++){
            var element = elements.Pop();

            string name = string.Empty; 

            try{
                bool hasFigure = element.Elements.Any(p => p == SharedKernel.Enums.EntityTypeEnum.Figure);
                if(hasFigure){
                    _logger.LogDebug("Trying to get {0}. name from generated names collection", nameIteration);

                    name = names?[nameIteration] ?? string.Empty;
                    nameIteration++;

                    _logger.LogInformation("Extracted name for flat conversion is {0}", name);
                }
            }
            catch (Exception ex){
                _logger.LogError(ex, "There is an error ocurred while getting name from IList collection");
            }

            var flatten = _filler.FillFlattenWithElements(element, new(code, date, name: name), name);

            flattens.Add(flatten);
        }
        return flattens;
    }
}
