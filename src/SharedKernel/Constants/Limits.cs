using System;

namespace SharedKernel.Constants;

public static class Limits
{
    public const int MaxCustomerNoteLength = 2000;
    public const int MinCustomerNoteLength = 20;
    public const int MaxPhotoCount = 20;
    public const int MinPhotoCount = 1;
    public const int MaxPetCountInProduct = 1;
    public const int MaxAccessoriesInCouple = 2;
    public const int MaxAccessoriesInFamily = 10;
    public const int MaxAccessoriesInStandard = 1;
    public const int MaxAccessoriesInFigure = 1;
    public const int MaxFigureCountInProduct = 2;
    public const int MaxFiguresInStandard = 1;
    public const int MaxFiguresInCouple = 2;
    public const int MaxFiguresInFamily = 10;
    public const int MaxFiguresForNonFigureProduct = 0;
    public const int MaxProductsInOrder = 100;
}
