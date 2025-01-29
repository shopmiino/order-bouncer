using System;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using OrderBouncer.GoogleSheets.Interfaces.Repositories;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;

namespace OrderBouncer.GoogleSheets.Services.Repositories;

public class GoogleSheetsRepository : IGoogleSheetsRepository
{
    private readonly SheetsService _sheets;
    private readonly IConfiguration _configuration;
    private readonly IRowConverterService _converter;

    public GoogleSheetsRepository(SheetsService sheetsService, IConfiguration configuration, IRowConverterService converter){
        _sheets = sheetsService;
        _configuration = configuration;
        _converter = converter;
    }

    public async Task AddRow(string[] rowElements, string range)
    {
        ValueRange valueRange = new ValueRange{
            Values = [rowElements]
        };
        
        var request = _sheets.Spreadsheets.Values.Append(valueRange, _configuration["Settings:Google:Sheets:OrderTrackSpreadSheetId"], range);
        request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;
        await request.ExecuteAsync();
    }

    public async Task AddRowV2(OrderRow orderRow, string? Range = null)
    {
        RowData rowData = new RowData{
            Values = _converter.ConvertToCellDatas(orderRow),
        };

        BatchUpdateSpreadsheetRequest request = new(){
            Requests = new List<Request>{
                new Request{
                    AppendCells = new AppendCellsRequest{
                        SheetId = Convert.ToInt32(_configuration["Settings:Google:Sheets:OrderTrackSpreadSheetGid"]),
                        Rows = [rowData],
                        Fields = "userEnteredValue,userEnteredFormat.backgroundColor"
                    }
                }
            }
        };

        await _sheets.Spreadsheets.BatchUpdate(request, _configuration["Settings:Google:Sheets:OrderTrackSpreadSheetId"]).ExecuteAsync();
    }

    public Task DeleteRow(int row)
    {
        throw new NotImplementedException();
    }

    public Task<string[]?> GetRow(int row)
    {
        throw new NotImplementedException();
    }

    public Task<string[]?> GetRowByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<string[]>?> GetRows(string range)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRow(int row, string[] rowElements)
    {
        throw new NotImplementedException();
    }
}
