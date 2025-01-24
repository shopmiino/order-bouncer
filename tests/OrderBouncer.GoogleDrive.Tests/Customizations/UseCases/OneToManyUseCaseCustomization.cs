using System;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.UseCases;

public class OneToManyUseCaseCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        Faker faker = new();

        var mock = fixture.Freeze<Mock<IOneToManyUseCase<BaseDto>>>();

        mock.Setup(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<BaseDto>(), It.IsAny<ICollection<string>>(), It.IsAny<CreationModes>()))
            .ReturnsAsync((int count) => [.. Enumerable.Range(0, count).Select( _ => faker.Random.Guid().ToString())]);
    }
}
