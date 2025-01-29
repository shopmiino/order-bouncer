using System;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Models;

namespace OrderBouncer.GoogleSheets.Services;

public class GoogleSheetsEngine : IGoogleSheetsEngine
{
    private readonly IGoogleSheetsRepository _repo;
    private readonly IRowFactory _rowFactory;
    private readonly IRowOrganizerService _organizer;
    private readonly IRowDiagramService _diagram;
    public GoogleSheetsEngine(IGoogleSheetsRepository repo, IRowOrganizerService organizer, IRowFactory rowFactory, IRowDiagramService diagram){
        _repo = repo;
        _organizer = organizer;
        _diagram = diagram;
        _rowFactory = rowFactory;
    }
    public async Task UploadOrder(OrderDto dto, CancellationToken cancellationToken)
    {
        IList<OrderRow> orderRows = [];

        ICollection<FlattenRowDto> flattenRows = _organizer.Organize(dto, cancellationToken);
        Stack<RowElements> organizedElements = _organizer.Organize(dto, cancellationToken);

        

        foreach(FlattenRowDto flat in flattenRows){
            OrderRow orderRow = _rowFactory.Create().From(flat).Build(); 
            orderRows.Add(orderRow);
        }

        orderRows = await _diagram.MarkRowDiagrams(orderRows);

        await _repo.AddRow(["John Doe", "2025-01-10", "ABC123", "Black", "Light", "Male", "true", "Type1", "Type2", "Print1", "Print2", "Pending", "false", "true", "Hat", "Dog", "Urgent delivery", "2025-01-20"], "A1:Z1");
    }
}
