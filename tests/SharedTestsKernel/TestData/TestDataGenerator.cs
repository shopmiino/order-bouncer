using System;
using Bogus;
using OrderBouncer.Domain.DTOs.Base;

namespace SharedTestsKernel.TestData;

public static class TestDataGenerator
{
    public static Faker<ProductDto> ProductDtoFaker => new Faker<ProductDto>()
        .RuleFor(p => p.Accessories, f => AccessoryDtoFaker.GenerateBetween(0, 5))
        .RuleFor(p => p.Pets, f => PetDtoFaker.GenerateBetween(0, 5))
        .RuleFor(p => p.Keychains, f => KeyChainDtoFaker.GenerateBetween(0, 5))
        .RuleFor(p => p.Figures, f => FigureDtoFaker.GenerateBetween(0, 5));

    public static Faker<BaseDto> BaseDtoFaker => new Faker<BaseDto>()
        .RuleFor(b => b.ImagePaths, f => [.. Enumerable.Range(1,8).Select(_ => f.Random.Guid().ToString())])
        .RuleFor(b => b.Note, f => f.Lorem.Paragraph());

    public static Faker<PetDto> PetDtoFaker => new Faker<PetDto>()
        .RuleFor(b => b.ImagePaths, f => [.. Enumerable.Range(1,8).Select(_ => f.Random.Guid().ToString())])
        .RuleFor(b => b.Note, f => f.Lorem.Paragraph());
    public static Faker<AccessoryDto> AccessoryDtoFaker => new Faker<AccessoryDto>()
        .RuleFor(b => b.ImagePaths, f => [.. Enumerable.Range(1,8).Select(_ => f.Random.Guid().ToString())])
        .RuleFor(b => b.Note, f => f.Lorem.Paragraph());
    public static Faker<FigureDto> FigureDtoFaker => new Faker<FigureDto>()
        .RuleFor(b => b.ImagePaths, f => [.. Enumerable.Range(1,8).Select(_ => f.Random.Guid().ToString())])
        .RuleFor(b => b.Accessories, f => AccessoryDtoFaker.Generate(f.Random.Int(0,2)))
        .RuleFor(b => b.Note, f => f.Lorem.Paragraph());
    public static Faker<KeychainDto> KeyChainDtoFaker => new Faker<KeychainDto>()
        .RuleFor(b => b.ImagePaths, f => [.. Enumerable.Range(1,8).Select(_ => f.Random.Guid().ToString())])
        .RuleFor(b => b.Note, f => f.Lorem.Paragraph());
}
