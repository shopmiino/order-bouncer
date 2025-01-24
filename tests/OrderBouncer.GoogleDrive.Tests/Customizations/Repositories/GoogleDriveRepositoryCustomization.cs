using System;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.GoogleDrive.Interfaces;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Repositories;

public class GoogleDriveRepositoryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        Faker faker = new();
        
        var mockRepo = fixture.Freeze<Mock<IGoogleDriveRepository>>();

        mockRepo.Setup(x => x.CreateFolder(It.IsAny<string>(), It.IsAny<string?>())).ReturnsAsync(faker.Random.Guid().ToString());

        mockRepo.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string?>()));

        mockRepo.Setup(x => x.BatchUploadFile(It.IsAny<ICollection<string>>(), It.IsAny<string?>()));
    }
}
