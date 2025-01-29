using System;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces.Services;

namespace OrderBouncer.GoogleSheets.Services;

public class RowDiagramService : IRowDiagramService
{
    public async Task<IList<OrderRow>> MarkRowDiagrams(IList<OrderRow> rows)
    {
        int rowCount = rows.Count;
        int middleCount = rowCount - 2;

        rows[0].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Opening);

        await Task.Run(() => {
            for(int i = 1; i < middleCount ; i++){
                rows[i].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Straight);
            }
        });

        rows[rowCount-1].Diagram.MarkAsDiagram(Constants.DiagramTypesEnum.Closing);
        return rows;
    }
}
