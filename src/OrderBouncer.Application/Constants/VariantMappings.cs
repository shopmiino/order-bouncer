using System;
using OrderBouncer.Domain.Variants;

namespace OrderBouncer.Application.Constants;

public static class VariantMappings
{
    public static Dictionary<long, SingleFigureVariant> SingleFigureVariantMappings = new(){
        {49563336212758, new()},
        {49563235877142, new(HasExtraPet: true)},
        {49563336179990, new(HasExtraAccessory: true)},
        {49562955710742, new(true, true)},
    };

    public static Dictionary<long, CoupleFigureVariant> CoupleFigureVariantMappings = new(){
        {49570148811030, new()},
        {49570148778262, new(HasExtraPet: true)},
        {49570243117334, new(HasExtraAccessoryForSecond: true)},
        {49570243084566, new(HasExtraAccessoryForSecond: true, HasExtraPet: true)},
        {49570148679958, new(HasExtraAccessoryForFirst: true)},
        {49570148647190, new(HasExtraAccessoryForFirst: true, HasExtraPet: true)},
        {49570243051798, new(HasExtraAccessoryForFirst: true, HasExtraAccessoryForSecond: true)},
        {49570243019030, new(true, true, true)},
    };
}
