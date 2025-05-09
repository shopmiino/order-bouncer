using System;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces.Services;

public interface IRowDiagramService
{
    public List<OrderRow> MarkRowDiagrams(List<OrderRow> rows);
}
