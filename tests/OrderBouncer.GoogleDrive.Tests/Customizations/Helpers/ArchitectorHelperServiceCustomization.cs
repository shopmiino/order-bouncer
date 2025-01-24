using System;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Helpers;

public class ArchitectorHelperServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        Faker faker = new();

        var mock = fixture.Freeze<Mock<IArchitectorHelperService>>();

        mock.Setup(x => x.CollectionInitializer(It.IsAny<FolderNamesEnum>())).Returns((FolderNamesEnum type) =>
        {
            switch (type)
            {
                case FolderNamesEnum.Accessory:
                    return prod => prod.Accessories as ICollection<BaseDto>;
                case FolderNamesEnum.Pet:
                    return prod => prod.Pets as ICollection<BaseDto>;
                case FolderNamesEnum.Figure:
                    return prod => prod.Figures as ICollection<BaseDto>;
                case FolderNamesEnum.Keychain:
                    return prod => prod.Keychains as ICollection<BaseDto>;
                default:
                    return _ => throw new ArgumentException("Error while setting collection for products");
            }
        });

        mock.Setup(x => x.Generate(It.IsAny<ICollection<ProductDto>>(), It.IsAny<FolderNamesEnum>(), It.IsAny<string>()));

        mock.Setup(x => x.GenerateGeneric(It.IsAny<int>(), It.IsAny<FolderNamesEnum>(), It.IsAny<string?>())).ReturnsAsync(faker.Random.Guid().ToString());

        mock.Setup(x => x.GetCount(It.IsAny<ICollection<BaseDto>?>())).Returns((ICollection<BaseDto>? a) => a is null ? 0 : a.Count);
    }
}
