using System;
using System.ComponentModel;
using OrderBouncer.Application.Constants;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Variants;

namespace OrderBouncer.Application.Services.Converters;

public class SingleFigureDtoLineItemConverterService : ILineItemsConverterService<FigureDto>
{
    public FigureDto Convert(LineItem lineItem)
    {
        SingleFigureVariant variant = VariantMappings.SingleFigureVariantMappings[lineItem.VariantId];

        FigureDto figureDto = new();
        return figureDto;
    }
}
