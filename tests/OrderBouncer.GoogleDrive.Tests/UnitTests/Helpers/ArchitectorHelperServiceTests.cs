using System;
using AutoFixture;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;
using OrderBouncer.GoogleDrive.Tests.Fixtures;

namespace OrderBouncer.GoogleDrive.Tests.UnitTests.Helpers;

public class ArchitectorHelperServiceTests
{
    private readonly ArchitectorHelperServiceFixture _fixture;
    public ArchitectorHelperServiceTests(){
        _fixture = new();
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
}
