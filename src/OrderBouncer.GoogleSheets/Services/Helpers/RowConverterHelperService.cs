using System;
using Google.Apis.Sheets.v4.Data;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;

namespace OrderBouncer.GoogleSheets.Services.Helpers;

public class RowConverterHelperService : IRowConverterHelperService
{
    public CellData CellToSpreadSheetCell(Cell cell)
    {
        return new CellData{
            UserEnteredValue = new ExtendedValue { StringValue = cell.InnerText ?? "" },
            UserEnteredFormat = new CellFormat
            {
                BackgroundColor = ColorsMappings.GetSheetsColor(cell.StandardColor)
            }
        };
    }
}
