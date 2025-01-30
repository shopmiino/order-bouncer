using System;
using Bogus;
using OrderBouncer.Domain.DTOs.Base;

namespace SharedTestsKernel.TestData;

public static class ExactTestDataGenerator
{
    public static Faker<ProductDto> ProductDtoFaker(int acc, int pet, int key, int fig) => new Faker<ProductDto>()
        .RuleFor(p => p.Accessories, f => TestDataGenerator.AccessoryDtoFaker.Generate(acc))
        .RuleFor(p => p.Pets, f => TestDataGenerator.PetDtoFaker.Generate(pet))
        .RuleFor(p => p.Keychains, f => TestDataGenerator.KeyChainDtoFaker.Generate(key))
        .RuleFor(p => p.Figures, f => TestDataGenerator.FigureDtoFaker.Generate(fig));


    public static Faker<OrderDto> OrderDtoFaker(int acc, int pet, int key, int fig) => new Faker<OrderDto>()
        .RuleFor(o => o.Products, f => ProductDtoFaker(acc, pet, key, fig).Generate(1))
        .RuleFor(o => o.Note, f => f.Lorem.Paragraph())
        .RuleFor(o => o.ShopifyOrderID, f => f.Random.Int(1000,1100).ToString());
}
