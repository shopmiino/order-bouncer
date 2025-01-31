using System;
using Google.Apis.Sheets.v4.Data;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces.Helpers;

public interface IRowConverterHelperService
{
    public CellData CellToSpreadSheetCell(Cell cell);
}
