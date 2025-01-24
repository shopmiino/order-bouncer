using System;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;

namespace OrderBouncer.GoogleDrive.Tests.Customizations.Helpers;

public class NamingHelperServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mockRepo = fixture.Freeze<Mock<INamingHelperService>>();

        mockRepo.Setup(x => x.GenerateFolderName(It.IsAny<FolderNamesEnum>(), It.IsAny<int>()))
            .Returns(
                (FolderNamesEnum name, int a) => {
                    string countName = a <= 0 ? "(Yok)" : $"({a} Tane)";
                    return $"{FolderNames.Names[name]} {countName}";
                });
                
        mockRepo.Setup(x => x.NamingMethod(FolderNamesEnum.Id)).Returns(index => (index + 1).ToString());

        mockRepo.Setup(x => x.NamingMethod(It.Is<FolderNamesEnum>(name => name != FolderNamesEnum.Id))).Returns((FolderNamesEnum name) => _ => FolderNames.Names[name]);
    }
}
