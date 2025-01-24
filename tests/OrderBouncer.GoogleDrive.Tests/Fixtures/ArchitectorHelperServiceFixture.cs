using System;
using AutoFixture;
using OrderBouncer.GoogleDrive.Tests.Customizations.Repositories;
using SharedTestsKernel.Customizations.TestData;


namespace OrderBouncer.GoogleDrive.Tests.Fixtures;

public class ArchitectorHelperServiceFixture : IDisposable
{
    public IFixture Fixture {get;}
    public ArchitectorHelperServiceFixture(){
        Fixture = new Fixture();

        Fixture.Customize(new GoogleDriveRepositoryCustomization());
        
        //data
        Fixture.Customize(new AccessoryDtoBogusCustomization());
    }
    public void Dispose()
    {
        
    }
}
