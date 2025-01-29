using System;
using OrderBouncer.GoogleSheets.Interfaces;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsRepository : IGoogleSheetsRepository
{
    private readonly SheetsService _sheets;
    private readonly IConfiguration _configuration;

    public GoogleSheetsRepository(SheetsService sheetsService, IConfiguration configuration){
        _sheets = sheetsService;
        _configuration = configuration;
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
