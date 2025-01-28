using System;
using OrderBouncer.GoogleSheets.Constants;
using OrderBouncer.GoogleSheets.Entities;

namespace OrderBouncer.GoogleSheets.Interfaces;

public interface ICellFactory
{
    public Cell CreateBlank();
    public Cell CreateDate(DateTime date);
    public Cell CreateDiagram(DiagramTypesEnum diagram);
    public Cell CreateText(string text);
}
