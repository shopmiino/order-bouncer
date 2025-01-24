using System;
using AutoFixture;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Architectors;

public class GoogleDriveArchitectorCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IGoogleDriveArchitector>>();

        mock.Setup(x => x.ExecuteAsync(It.IsAny<OrderDto>(), It.IsAny<ICollection<FolderNamesEnum>>(), It.IsAny<CancellationToken>()));
    }
}
