using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using Bogus;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.UseCases;

public class UseCaseCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        Faker faker = new();
        fixture.Customize(new AutoMoqCustomization());

        var manyToManyMock = fixture.Freeze<Mock<IManyToManyUseCase<BaseDto>>>();
        var manyToOneMock = fixture.Freeze<Mock<IManyToOneUseCase<BaseDto>>>();
        var oneToManyMock = fixture.Freeze<Mock<IOneToManyUseCase<BaseDto>>>();

        manyToManyMock.Setup(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<ICollection<BaseDto>>(), It.IsAny<List<string>>(), It.IsAny<CreationModes>()))
            .ReturnsAsync([.. Enumerable.Range(0, 5).Select( _ => faker.Random.Guid().ToString())]);

        manyToOneMock.Setup(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<ICollection<BaseDto>>(), It.IsAny<string>(), It.IsAny<CreationModes>()))
            .ReturnsAsync([.. Enumerable.Range(0, 5).Select( _ => faker.Random.Guid().ToString())]);

        oneToManyMock.Setup(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<BaseDto>(), It.IsAny<ICollection<string>>(), It.IsAny<CreationModes>()))
            .ReturnsAsync([.. Enumerable.Range(0, 5).Select( _ => faker.Random.Guid().ToString())]);
    }
}
