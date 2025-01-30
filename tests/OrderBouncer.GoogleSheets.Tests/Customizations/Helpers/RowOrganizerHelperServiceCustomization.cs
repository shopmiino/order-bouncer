using System;
using AutoFixture;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Helpers;

public class RowOrganizerHelperServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowOrganizerHelper>>();
        
        mock.Setup(x => x.GetElementCounts(It.IsAny<OrderDto>())).Returns(new Dictionary<EntityTypeEnum, int>(){
         {EntityTypeEnum.Accessory, 4},
         {EntityTypeEnum.Figure, 1},
         {EntityTypeEnum.Keychain, 0},
         {EntityTypeEnum.Pet, 2}   
        });

        mock.Setup(x => x.GetElementsHasAtLeastOne(It.IsAny<Dictionary<EntityTypeEnum, int>>())).Returns(new Models.RowElements(){
            Elements = [EntityTypeEnum.Accessory, EntityTypeEnum.Figure, EntityTypeEnum.Pet],
            Count = 1
        });

        mock.Setup(x => x.GetHighestCountElement(It.IsAny<Dictionary<EntityTypeEnum, int>>())).Returns(new KeyValuePair<EntityTypeEnum, int>(key:EntityTypeEnum.Accessory, 4));

        mock.Setup(x => x.RemoveOneFromEach(It.IsAny<Dictionary<EntityTypeEnum, int>>())).Returns(true);

        mock.Setup(x => x.GetCount(It.IsAny<ICollection<BaseDto>?>())).Returns(2);
        
    }
}
