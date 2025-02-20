using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsConverterService<TOut>
{
    public Task<TOut> Convert(LineItem lineItem);
    public Task<(TOut, PetDto?)> ConvertWithExtraPet(LineItem lineItem);
    public Task<(TOut, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem);
    public Task<(TOut, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem);
    public Task<(TOut, ICollection<PetDto>?, ICollection<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem);
}
