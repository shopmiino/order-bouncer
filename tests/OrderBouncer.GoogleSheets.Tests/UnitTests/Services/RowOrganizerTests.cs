using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Models;
using OrderBouncer.GoogleSheets.Services;
using OrderBouncer.GoogleSheets.Services.Helpers;
using SharedKernel.Enums;
using SharedTestsKernel.TestData;

namespace OrderBouncer.GoogleSheets.Tests.UnitTests.Services;

public class RowOrganizerTests
{
    [Fact]
    public void Organize_2Fig5Acc3Key2PetDTO_ReturnsOrganizedElements(){
        OrderDto data = ExactTestDataGenerator.OrderDtoFaker(fig: 2, acc: 5, key: 3, pet: 2).Generate();

        Stack<RowElements> expected = new();

        expected.Push(new(){Elements = [EntityTypeEnum.Figure, EntityTypeEnum.Accessory, EntityTypeEnum.Keychain, EntityTypeEnum.Pet], Count = 1});
        expected.Push(new(){Elements = [EntityTypeEnum.Figure, EntityTypeEnum.Accessory, EntityTypeEnum.Keychain, EntityTypeEnum.Pet], Count = 1});
        expected.Push(new(){Elements = [EntityTypeEnum.Accessory, EntityTypeEnum.Keychain], Count = 1});
        expected.Push(new(){Elements = [EntityTypeEnum.Accessory], Count = 1});
        expected.Push(new(){Elements = [EntityTypeEnum.Accessory], Count = 1});

        IRowOrganizerHelper helper = new RowOrganizerHelper();
        IRowOrganizerService actualService = new RowOrganizer(helper);

        var result = actualService.Organize(data, CancellationToken.None);
        
        //Expected
        //Figure, Accessory, Keychain, Pet
        //Figure, Accessory, Keychain, Pet
        //Accessory, Keychain
        //Accessory
        //Accessory
        Assert.Equivalent(expected, result);
    }
}
