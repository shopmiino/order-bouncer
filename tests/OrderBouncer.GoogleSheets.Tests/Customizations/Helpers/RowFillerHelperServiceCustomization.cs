using System;
using AutoFixture;
using Moq;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Helpers;

public class RowFillerHelperServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowFillerHelperService>>();
        mock.Setup(x => x.HasCondition(It.IsAny<bool>())).Returns((bool condition) => {
            return condition ? ColorsEnum.Yellow : ColorsEnum.White;
        });
    }
}
