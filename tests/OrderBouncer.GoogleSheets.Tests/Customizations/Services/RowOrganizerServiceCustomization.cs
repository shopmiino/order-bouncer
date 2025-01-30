using System;
using AutoFixture;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Tests.TestData;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Services;

public class RowOrganizerServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowOrganizerService>>();

        mock.Setup(x => x.Organize(It.IsAny<OrderDto>(), It.IsAny<CancellationToken>())).Returns(() => {
            var stack = new Stack<Models.RowElements>();
            
            stack.Push(DataGenerator.GenerateRowElements());
            stack.Push(DataGenerator.GenerateRowElements());
            stack.Push(DataGenerator.GenerateRowElements());

            return stack;
    });
    }
}
