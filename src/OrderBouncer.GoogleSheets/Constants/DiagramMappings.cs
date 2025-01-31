using System;

namespace OrderBouncer.GoogleSheets.Constants;

public static class DiagramMappings
{
    private static Dictionary<DiagramTypesEnum, string> _diagramMappings = new(){
        {DiagramTypesEnum.Opening, "╔═══"},
        {DiagramTypesEnum.Straight, "║       "},
        {DiagramTypesEnum.Closing, "╚═══"},
        {DiagramTypesEnum.Single, "════"}
    };
    public static string GetDiagramString(DiagramTypesEnum diagram){
        return _diagramMappings[diagram];
    }
}
