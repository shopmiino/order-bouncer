using System;
using SharedKernel.Enums;

namespace OrderBouncer.GoogleSheets.Models;

public struct RowElements
{
    public int Count;
    public EntityTypeEnum[] Elements;
}
