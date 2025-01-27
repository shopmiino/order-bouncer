using OrderBouncer.GoogleSheets.Constants;

namespace OrderBouncer.GoogleSheets.DTOs;

public record class CellDto
{
    public string Name {get;}
    public string? InnerText {get;}
    public bool? Enabled {get;}
    public ColorsEnum StandardColor {get;}
    public ColorsEnum? DisabledColor {get;}
    public ColorsEnum? EnabledColor {get;}
    public Type? TypeFormat {get;}
    public DiagramTypesEnum? DiagramType {get;}
    public CellTypesEnum CellType {get;}

    public CellDto(string name, ColorsEnum standardColor = ColorsEnum.White, bool? enabled = null, ColorsEnum? enabledColor = null, ColorsEnum? disabledColor = null, string? innerText = null, Type? typeFormat = null, CellTypesEnum? cellType = null, DiagramTypesEnum? diagramType = null){
        Name = name;
        InnerText = innerText;
        Enabled = enabled;
        StandardColor = standardColor;
        DisabledColor = disabledColor;
        EnabledColor = enabledColor;
        TypeFormat = typeFormat;
        DiagramType = diagramType;
        
        CellType = cellType ?? CellTypesEnum.Empty;

        if(cellType == CellTypesEnum.Diagram && diagramType is null){
            throw new InvalidDataException($"{nameof(DiagramType)} can not be null if {nameof(CellType)} is {CellTypesEnum.Diagram.ToString()}");
        }
    }
}
