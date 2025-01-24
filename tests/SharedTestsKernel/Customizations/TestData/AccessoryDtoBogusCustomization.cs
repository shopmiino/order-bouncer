using System;
using AutoFixture;
using Bogus;
using SharedTestsKernel.TestData;

namespace SharedTestsKernel.Customizations.TestData;

public class AccessoryDtoBogusCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => TestDataGenerator.AccessoryDtoFaker.GenerateBetween(0, 5));
    }
}
