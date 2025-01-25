using System;
using AutoFixture;
using OrderBouncer.GoogleDrive.Tests.Customizations.Helpers;
using OrderBouncer.GoogleDrive.Tests.Customizations.Repositories;
using OrderBouncer.GoogleDrive.Tests.Customizations.UseCases;

namespace OrderBouncer.GoogleDrive.Tests.Fixtures.Helpers;

public class ArchitectorHelperServiceFixture
{
    public IFixture Fixture {get;}
    public ArchitectorHelperServiceFixture(){
        Fixture = new Fixture();

        Fixture.Customize(new GoogleDriveRepositoryCustomization());
        Fixture.Customize(new UseCaseCustomization());
        Fixture.Customize(new NamingHelperServiceCustomization());
    }
}
