using System;
using AutoFixture;
using Moq;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;
using OrderBouncer.GoogleSheets.Tests.TestData;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Services;

public class RowFillerServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowFillerService>>();

        mock.Setup(x => x.FillFlattenWithElements(It.IsAny<RowElements>(), It.IsAny<FlattenRowDto>())).Returns(
            new FlattenRowDto("orderCode", DateTime.MinValue, hasAccessory: true, hasPet: true)
        );

        mock.Setup(x => x.FillWithFlatten(It.IsAny<FlattenRowDto>(), It.IsAny<OrderRow>())).Returns(
            DataGenerator.GenerateOrderRow()
        );
    }
}
