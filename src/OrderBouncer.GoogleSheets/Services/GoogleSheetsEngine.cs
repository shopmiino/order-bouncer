using System;
using Google.Apis.Sheets.v4.Data;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Interfaces.Factories;
using OrderBouncer.GoogleSheets.Interfaces.Repositories;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsEngine : IGoogleSheetsEngine
{
    private readonly IGoogleSheetsRepository _repo;
    private readonly IRowFactory _rowFactory;
    private readonly IRowOrganizerService _organizer;
    private readonly IRowDiagramService _diagram;
    private readonly IRowConverterService _converter;
    public GoogleSheetsEngine(IGoogleSheetsRepository repo, IRowOrganizerService organizer, IRowFactory rowFactory, IRowDiagramService diagram, IRowConverterService converter){
        _repo = repo;
        _organizer = organizer;
        _diagram = diagram;
        _rowFactory = rowFactory;
        _converter = converter;
    }
    public async Task UploadOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        List<OrderRow> orderRows = [];

        Stack<RowElements> organizedElements = _organizer.Organize(dto, cancellationToken);
        
        List<FlattenRowDto> flattenRows = _converter.ConvertToFlatten(organizedElements, dto);

        foreach(FlattenRowDto flat in flattenRows){
            OrderRow orderRow = _rowFactory.Create().From(flat).Build(); 
            orderRows.Add(orderRow);
        }


        orderRows.Reverse();
        orderRows = _diagram.MarkRowDiagrams(orderRows);
        
        List<RowData> rowDatas = [];

        foreach(OrderRow row in orderRows){
            RowData rowData = new RowData(){
                Values = _converter.ConvertToCellDatas(row),
            };

            rowDatas.Add(rowData);
        }

        await _repo.AddRowV2(rowDatas);
    }
}
