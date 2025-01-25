using System;
using System.Threading.Tasks;
using AutoFixture;
using Bogus;
using Moq;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Constants;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;
using OrderBouncer.GoogleDrive.Services.Helpers;
using OrderBouncer.GoogleDrive.Tests.Fixtures;
using OrderBouncer.GoogleDrive.Tests.Fixtures.Helpers;
using SharedTestsKernel.TestData;

namespace OrderBouncer.GoogleDrive.Tests.UnitTests.Helpers;

public class ArchitectorHelperServiceTests
{
    private readonly ArchitectorHelperServiceFixture _fixture;
    private readonly ArchitectorHelperService _actualService;
    public ArchitectorHelperServiceTests(){
        _fixture = new();

        var repo = _fixture.Fixture.Create<IGoogleDriveRepository>();
        var namingHelper = _fixture.Fixture.Create<INamingHelperService>();
        var manyToMany = _fixture.Fixture.Create<IManyToManyUseCase<BaseDto>>();
        var manyToOne = _fixture.Fixture.Create<IManyToOneUseCase<BaseDto>>();
        var oneToMany = _fixture.Fixture.Create<IOneToManyUseCase<BaseDto>>();

        _actualService = new ArchitectorHelperService(repo, namingHelper, oneToMany, manyToOne, manyToMany);

    }

    [Fact]
    public void TestName()
    {
        // Arrange
        var repo = _fixture.Fixture.Create<IGoogleDriveRepository>();
        var namingHelper = _fixture.Fixture.Create<INamingHelperService>();
        var manyToMany = _fixture.Fixture.Create<IManyToManyUseCase<BaseDto>>();
        var manyToOne = _fixture.Fixture.Create<IManyToOneUseCase<BaseDto>>();
        var oneToMany = _fixture.Fixture.Create<IOneToManyUseCase<BaseDto>>();
        // Act

        
    
        // Assert
    }

    [Theory]
    [InlineData(FolderNamesEnum.Accessory)]
    [InlineData(FolderNamesEnum.Pet)]
    [InlineData(FolderNamesEnum.Keychain)]
    [InlineData(FolderNamesEnum.Figure)]
    public void CollectionInitializer_Valid_ReturnsAssignable(FolderNamesEnum name){
        //arrange
        Faker faker = new();

        var collectionFunc = _actualService.CollectionInitializer(name);
        ProductDto productDto = TestDataGenerator.ProductDtoFaker.Generate();
      
        var result = collectionFunc.Invoke(productDto);
        //assert
        
        Assert.IsAssignableFrom<ICollection<BaseDto>?>(result);
    }

    [Theory]
    [InlineData(FolderNamesEnum.Images)]
    [InlineData(FolderNamesEnum.Id)]
    public void CollectionInitializer_Invalid_ThrowsArgumentException(FolderNamesEnum name){
        //arrange
        Faker faker = new();

        //act
        var collectionFunc = _actualService.CollectionInitializer(name);
        ProductDto productDto = TestDataGenerator.ProductDtoFaker.Generate();


        //assert
        Assert.Throws<ArgumentException>(() => collectionFunc.Invoke(productDto));
    }

    [Fact]
    public void Generate_NullCollection_ReturnsAsync(){
        Faker faker = new();

        var result = _actualService.Generate<ProductDto>(null, FolderNamesEnum.Pet, faker.Random.Guid().ToString());

        Assert.IsAssignableFrom<Task>(result);
    }

    [Fact]
    public async Task Generate_EmptyCollection_NoProcessing(){
        var mockManyToOne = _fixture.Fixture.Freeze<Mock<IManyToOneUseCase<BaseDto>>>();
        var mockManyToMany = _fixture.Fixture.Freeze<Mock<IManyToManyUseCase<BaseDto>>>();

        var repo = _fixture.Fixture.Create<IGoogleDriveRepository>();
        var namingHelper = _fixture.Fixture.Create<INamingHelperService>();
        var oneToMany = _fixture.Fixture.Create<IOneToManyUseCase<BaseDto>>();

        var service = new ArchitectorHelperService(repo, namingHelper, oneToMany, mockManyToOne.Object, mockManyToMany.Object);

        Faker faker = new();
        ICollection<ProductDto> emptyCollection = [];

        await service.Generate<ProductDto>(emptyCollection, FolderNamesEnum.Pet, faker.Random.Guid().ToString());

        mockManyToOne.Verify(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<ICollection<BaseDto>>(), It.IsAny<string>(), It.IsAny<CreationModes>()), Times.Never);        

        mockManyToMany.Verify(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), It.IsAny<ICollection<BaseDto>>(), It.IsAny<IList<string>>(), It.IsAny<CreationModes>()), Times.Never);        
    }

    [Theory]
    [InlineData(FolderNamesEnum.Accessory)]
    [InlineData(FolderNamesEnum.Keychain)]
    [InlineData(FolderNamesEnum.Pet)]
    [InlineData(FolderNamesEnum.Figure)]
    public async Task Generate_ValidEnums_CalledOnceWithProperCollectionType(FolderNamesEnum name){
        //arrange
        var mockManyToOne = _fixture.Fixture.Freeze<Mock<IManyToOneUseCase<BaseDto>>>();
        var mockManyToMany = _fixture.Fixture.Freeze<Mock<IManyToManyUseCase<BaseDto>>>();

        var repo = _fixture.Fixture.Create<IGoogleDriveRepository>();
        var namingHelper = _fixture.Fixture.Create<INamingHelperService>();
        var oneToMany = _fixture.Fixture.Create<IOneToManyUseCase<BaseDto>>();

        Faker faker = new();

        var service = new ArchitectorHelperService(repo, namingHelper, oneToMany, mockManyToOne.Object, mockManyToMany.Object);

        //act
        ProductDto productDto = TestDataGenerator.ProductDtoFaker.Generate();

        ICollection<ProductDto> productDtos = [productDto];

        await service.Generate<ProductDto>(productDtos, name, faker.Random.Guid().ToString());

        var init = service.CollectionInitializer(name);
        var baseColl = init(productDto);

        //assert
        mockManyToOne.Verify(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), baseColl, It.IsAny<string>(), It.IsAny<CreationModes>()), Times.Exactly(1));        

        mockManyToMany.Verify(x => x.ExecuteAsync(It.IsAny<FolderNamesEnum>(), baseColl, It.IsAny<IList<string>>(), It.IsAny<CreationModes>()), Times.Exactly(1));  
    }

}
