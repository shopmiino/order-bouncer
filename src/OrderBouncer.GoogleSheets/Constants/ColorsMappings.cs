using System;
using SheetsColor = Google.Apis.Sheets.v4.Data;
using OrderBouncer.GoogleSheets.ValueObjects;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OrderBouncer.GoogleSheets.Tests")]

namespace OrderBouncer.GoogleSheets.Constants;

internal static class ColorsMappings
{
    public static Dictionary<ColorsEnum, Color> Colors = new(){
        {ColorsEnum.Blue, new(ColorsEnum.Blue.ToString(), 0.259f, 0.522f, 0.957f, 1f)},
        {ColorsEnum.Green, new(ColorsEnum.Green.ToString(), 0.204f, 0.533f, 0.325f, 1f)},
        {ColorsEnum.Red, new(ColorsEnum.Red.ToString(), 0.918f, 0.263f, 0.208f, 1f)},
        {ColorsEnum.Yellow, new(ColorsEnum.Yellow.ToString(), 0.984f, 0.737f, 0.016f, 1f)},
        {ColorsEnum.White, new(ColorsEnum.White.ToString(), 1f, 1f, 1f, 1f)},
        {ColorsEnum.Black, new(ColorsEnum.Black.ToString(), 0f, 0f, 0f, 1f)},
        {ColorsEnum.Gray, new(ColorsEnum.Gray.ToString(), 0.6f, 0.6f, 0.6f, 1f)},
        {ColorsEnum.LightGray, new(ColorsEnum.LightGray.ToString(), 0.839f, 0.839f, 0.839f, 1f)},
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
