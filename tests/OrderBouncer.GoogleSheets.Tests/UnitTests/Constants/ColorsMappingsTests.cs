using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Tests.Constants.ExpectedColors;

namespace OrderBouncer.GoogleSheets.Tests.UnitTests.Constants;

public class ColorsMappingsTests
{
    [Fact]
    public void GetSheetsColor_RedEnum_ReturnsProperColor(){
        var result = ColorsMappings.GetSheetsColor(ColorsEnum.Red);

        ExpectedRed expected = new();

        Assert.Equal(expected.Red, result.Red);
        Assert.Equal(expected.Green, result.Green);
        Assert.Equal(expected.Blue, result.Blue);
        Assert.Equal(expected.Alpha, result.Alpha);
    }
}
