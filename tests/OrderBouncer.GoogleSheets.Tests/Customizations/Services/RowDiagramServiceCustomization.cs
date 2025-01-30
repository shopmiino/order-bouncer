using System;
using AutoFixture;
using Moq;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Tests.TestData;

namespace OrderBouncer.GoogleSheets.Tests.Customizations.Services;

public class RowDiagramServiceCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mock = fixture.Freeze<Mock<IRowDiagramService>>();
        mock.Setup(x => x.MarkRowDiagrams(It.IsAny<IList<OrderRow>>())).Returns([
            DataGenerator.GenerateOrderRow().SetDiagram(new("Diagram", cellType: CellTypesEnum.Diagram, diagramType: DiagramTypesEnum.Opening)),
            DataGenerator.GenerateOrderRow().SetDiagram(new("Diagram", cellType: CellTypesEnum.Diagram, diagramType: DiagramTypesEnum.Straight)),
            DataGenerator.GenerateOrderRow().SetDiagram(new("Diagram", cellType: CellTypesEnum.Diagram, diagramType: DiagramTypesEnum.Closing)),
        ]);
    }
}
