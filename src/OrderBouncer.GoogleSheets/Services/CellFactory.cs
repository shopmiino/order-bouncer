using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Entities;
using OrderBouncer.GoogleSheets.Interfaces;

namespace OrderBouncer.GoogleSheets.Services;

public class CellFactory : ICellFactory
{
    public Cell CreateBlank()
    {
        throw new NotImplementedException();
    }

    public Cell CreateDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Cell CreateDiagram(DiagramTypesEnum diagram)
    {
        throw new NotImplementedException();
    }

    public Cell CreateText(string text)
    {
        throw new NotImplementedException();
    }
}
