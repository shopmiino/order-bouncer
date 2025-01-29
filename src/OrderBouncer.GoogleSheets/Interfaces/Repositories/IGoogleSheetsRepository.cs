using System;
using OrderBouncer.GoogleSheets.DTOs;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces.Repositories;

public interface IGoogleSheetsRepository
{
    public Task AddRow(string[] rowElements, string range);
    public Task AddRowV2(OrderRow orderRow, string? Range = null);
    public Task UpdateRow(int row, string[] rowElements);
    public Task<string[]?> GetRow(int row);
    public Task<string[]?> GetRowByName(string name);
    public Task<ICollection<string[]>?> GetRows(string range);
    public Task DeleteRow(int row);
}
