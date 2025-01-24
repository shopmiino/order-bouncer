using System;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.GoogleDrive.Interfaces;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Helpers;

public class GoogleDriveRepositoryHelperCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        Faker faker = new();

        var mockRepo = fixture.Freeze<Mock<IGoogleDriveRepositoryHelper>>();

        mockRepo.Setup(x => x.GetParentId(It.IsAny<string?>())).ReturnsAsync([faker.Random.Guid().ToString()]);
    }
}
