using System;
using AutoFixture;
using Google.Apis.Sheets.v4.Data;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Services;

public class RowConverterServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowConverterService>>();

        mock.Setup(x => x.ConvertToCellDatas(It.IsAny<OrderRow>())).Returns([
            new CellData{
                UserEnteredValue = new ExtendedValue{StringValue = "innerText"},
                UserEnteredFormat = new CellFormat(){
                    BackgroundColor = ColorsMappings.GetSheetsColor(ColorsEnum.LightGray)
                }
            }
        ]);

        mock.Setup(x => x.ConvertToFlatten(It.IsAny<Stack<RowElements>>(), It.IsAny<OrderDto>())).Returns([
            new FlattenRowDto("a1", DateTime.MinValue, hasAccessory: true, hasPet: true, hasKeychain: true),
            new FlattenRowDto("a1", DateTime.MinValue, hasAccessory: true, hasPet: true, hasKeychain: true),
            new FlattenRowDto("a1", DateTime.MinValue, hasAccessory: true, hasPet: true, hasKeychain: true),
            new FlattenRowDto("a1", DateTime.MinValue, hasAccessory: true, hasPet: true, hasKeychain: true),
        ]);
    }
}
