using System;
using SheetsColor = Google.Apis.Sheets.v4.Data;
using OrderBouncer.GoogleSheets.ValueObjects;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OrderBouncer.GoogleSheets.Tests")]

namespace OrderBouncer.GoogleSheets.Constants;

internal static class ColorsMappings
{
    public static Dictionary<ColorsEnum, Color> Colors = new(){
        {ColorsEnum.Blue, new(ColorsEnum.Blue.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.Green, new(ColorsEnum.Green.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.Red, new(ColorsEnum.Red.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.Yellow, new(ColorsEnum.Yellow.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.White, new(ColorsEnum.White.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.Black, new(ColorsEnum.Black.ToString(), 0f, 0f, 0f, 0f)},
    };
    public static Google.Apis.Sheets.v4.Data.Color GetSheetsColor(ColorsEnum color){
        Color temp = Colors[color];
        return new(){
            Red = temp.R,
            Green = temp.G,
            Blue = temp.B,
            Alpha = temp.A,
        };
    }
}
