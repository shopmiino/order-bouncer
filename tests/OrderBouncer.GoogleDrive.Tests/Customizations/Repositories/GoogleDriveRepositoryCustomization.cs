using System;
using AutoFixture;
using Moq;
using OrderBouncer.GoogleDrive.Interfaces;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Repositories;

public class GoogleDriveRepositoryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mockRepo = fixture.Freeze<Mock<IGoogleDriveRepository>>();
        mockRepo.Setup(x => x.CreateFolder(It.IsAny<string>(), It.IsAny<string?>())).ReturnsAsync("mocked-folder-id");
    }
}
