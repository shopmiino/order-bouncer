using System;
using AutoFixture;
using Google.Apis.Sheets.v4.Data;
using Moq;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Helpers;

public class RowConverterHelperServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowConverterHelperService>>();
        mock.Setup(x => x.CellToSpreadSheetCell(It.IsAny<Cell>())).Returns(() => new CellData()
        {
            UserEnteredValue = new ExtendedValue { StringValue = "innerText" },
            UserEnteredFormat = new CellFormat
            {
                BackgroundColor = new Color{Red = 1f, Green = 1f, Blue = 1f, Alpha = 1f}
            }

        });
    }
}
