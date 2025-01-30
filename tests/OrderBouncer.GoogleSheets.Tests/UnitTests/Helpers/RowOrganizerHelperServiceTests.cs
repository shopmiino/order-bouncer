using System;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Models;
using OrderBouncer.GoogleSheets.Services.Helpers;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Tests.UnitTests.Helpers;

public class RowOrganizerHelperServiceTests
{
    [Fact]
    public void GetHighestCountElement_2Fig5Acc3Key1Pet_Returns5Acc(){
        Dictionary<EntityTypeEnum, int> data = new (){
            {EntityTypeEnum.Figure, 2},
            {EntityTypeEnum.Accessory, 5},
            {EntityTypeEnum.Keychain, 3},
            {EntityTypeEnum.Pet, 1},
        };

        IRowOrganizerHelper actual = new RowOrganizerHelper();

        KeyValuePair<EntityTypeEnum, int> expected = new(EntityTypeEnum.Accessory, 5);

        var result = actual.GetHighestCountElement(data); 

        Assert.Equal(expected, result);
    }

    [Fact]
    public void RemoveOneFromEach_1Fig4Acc2Key0Pet_Returns0Fig3Acc1Key0Pet(){
        Dictionary<EntityTypeEnum, int> data = new (){
            {EntityTypeEnum.Figure, 1},
            {EntityTypeEnum.Accessory, 4},
            {EntityTypeEnum.Keychain, 2},
            {EntityTypeEnum.Pet, 0},
        };

        Dictionary<EntityTypeEnum, int> expected = new (){
            {EntityTypeEnum.Figure, 0},
            {EntityTypeEnum.Accessory, 3},
            {EntityTypeEnum.Keychain, 1},
            {EntityTypeEnum.Pet, 0},
        };

        IRowOrganizerHelper actualService = new RowOrganizerHelper();

        var result = actualService.RemoveOneFromEach(data);

        Assert.Equal(expected, data);
    }

    [Fact]
    public void GetElementsHasAtLeastOne_1Fig4Acc2Key0Pet_ReturnsFigAccKey(){
        Dictionary<EntityTypeEnum, int> data = new (){
            {EntityTypeEnum.Figure, 1},
            {EntityTypeEnum.Accessory, 4},
            {EntityTypeEnum.Keychain, 2},
            {EntityTypeEnum.Pet, 0},
        };

        RowElements expected = new(){Elements = [EntityTypeEnum.Figure, EntityTypeEnum.Accessory,EntityTypeEnum.Keychain], Count = 1};

        IRowOrganizerHelper actualService = new RowOrganizerHelper();

        var result = actualService.GetElementsHasAtLeastOne(data);

        Assert.Contains(EntityTypeEnum.Figure, result.Elements);
        Assert.Contains(EntityTypeEnum.Accessory, result.Elements);
        Assert.Contains(EntityTypeEnum.Keychain, result.Elements);

        Assert.Equal(expected.Elements, result.Elements);
    }
}
