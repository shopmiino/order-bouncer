using System;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IRowDiagramService
{
    public Task<IList<OrderRow>> MarkRowDiagrams(IList<OrderRow> rows);
}
