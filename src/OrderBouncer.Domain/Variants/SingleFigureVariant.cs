using System;

namespace OrderBouncer.Domain.Variants;

public record class SingleFigureVariant(bool HasExtraAccessory = false, bool HasExtraPet = false) 
{
}
