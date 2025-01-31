using System;
using AutoFixture;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Services;

public class GoogleSheetsEngineCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IGoogleSheetsEngine>>();
        mock.Setup(x => x.UploadOrder(It.IsAny<OrderDto>(), It.IsAny<CancellationToken>()));
    }
}
