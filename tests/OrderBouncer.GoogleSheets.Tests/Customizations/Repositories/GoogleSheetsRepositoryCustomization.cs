using System;
using AutoFixture;
using Google.Apis.Sheets.v4.Data;
using Moq;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Repositories;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Repositories;

public class GoogleSheetsRepositoryCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IGoogleSheetsRepository>>();
        mock.Setup(x => x.AddRowV2(It.IsAny<IList<RowData>>(), It.IsAny<string?>()));
    }
}
