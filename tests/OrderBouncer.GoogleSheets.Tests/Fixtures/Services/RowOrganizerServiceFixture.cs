using System;
using AutoFixture;

namespace OrderBouncer.GoogleSheets.Tests.Fixtures.Services;

public class RowOrganizerServiceFixture
{
    public IFixture Fixture;

    public RowOrganizerServiceFixture(){
        Fixture = new Fixture();
    }
}
