using System;
using AutoFixture;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Interfaces.Services;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Services;

public class GoogleDriveEngineCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IGoogleDriveEngine>>();

        mock.Setup(x => x.UploadOrder(It.IsAny<OrderDto>(), It.IsAny<CancellationToken>())); 
    }
}
