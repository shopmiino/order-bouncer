using System;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface IGoogleSheetsRepository
{
    public Task AddRow(string[] rowElements, string range);
    public Task UpdateRow(int row, string[] rowElements);
    public Task<string[]?> GetRow(int row);
    public Task<string[]?> GetRowByName(string name);
    public Task<ICollection<string[]>?> GetRows(string range);
    public Task DeleteRow(int row);
}
