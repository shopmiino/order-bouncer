using OrderBouncer.GoogleSheets.Constants;

namespace OrderBouncer.GoogleSheets.Entities;

public class Cell
{
    public string Name { get; private set;}
    public string? InnerText { get; private set;}
    public bool? Enabled { get; private set;}
    public ColorsEnum StandardColor { get; private set;}
    public ColorsEnum? DisabledColor { get; private set;}
    public ColorsEnum? EnabledColor { get; private set;}
    public Type? TypeFormat { get; private set;}
    public DiagramTypesEnum? DiagramType { get; private set; }
    public CellTypesEnum CellType { get; private set; }

    public Cell(string name, ColorsEnum standardColor = ColorsEnum.White, bool? enabled = null, ColorsEnum? enabledColor = null, ColorsEnum? disabledColor = null, string? innerText = null, Type? typeFormat = null, CellTypesEnum? cellType = null, DiagramTypesEnum? diagramType = null)
    {
        Name = name;
        InnerText = innerText;
        Enabled = enabled;
        StandardColor = standardColor;
        DisabledColor = disabledColor;
        EnabledColor = enabledColor;
        TypeFormat = typeFormat;
        DiagramType = diagramType;

        CellType = cellType ?? CellTypesEnum.Empty;

        if (cellType == CellTypesEnum.Diagram && diagramType is null)
        {
            throw new InvalidDataException($"{nameof(DiagramType)} can not be null if {nameof(CellType)} is {CellTypesEnum.Diagram.ToString()}");
        }
    }

    public Cell MarkAsDiagram(DiagramTypesEnum diagram){
        Name = "Diagram";
        CellType = CellTypesEnum.Diagram;
        DiagramType = diagram;
        return this;
    }

    public Cell MarkAsDate(DateTime date){
        Name = "Date";
        CellType = CellTypesEnum.Date;
        InnerText = date.ToString();
        return this;
    }

    public Cell MarkAsOrderCode(string code){
        Name = "OrderCode";
        CellType = CellTypesEnum.OrderCode;
        InnerText = code;
        return this;
    }
}
