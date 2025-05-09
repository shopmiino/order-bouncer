using System;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;

namespace OrderBouncer.GoogleSheets.Services;

public class RowDiagramService : IRowDiagramService
{
    public List<OrderRow> MarkRowDiagrams(List<OrderRow> rows)
    {
        int rowCount = rows.Count;
        int middleCount = rowCount - 2;

        if(rowCount == 1){
            rows[0].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Single);
            return rows;
        }

        rows[0].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Opening);


        for (int i = 1; i <= middleCount; i++)
        {
            rows[i].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Straight);
        }


        rows[rowCount - 1].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Closing);
        return rows;
    }
}
