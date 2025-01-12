using System;
using Google.Apis.Sheets.v4;

namespace OrderBouncer.GoogleSheets;

public class GoogleSheetsManager
{
    private readonly SheetsService _sheetsService;

    public GoogleSheetsManager(SheetsService sheetsService){
        _sheetsService = sheetsService;
    }
}
