using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsConverterService<TOut>
{
    public Task<TOut> Convert(LineItem lineItem, Guid scopeId);
    public Task<(TOut, PetDto?)> ConvertWithExtraPet(LineItem lineItem, Guid scopeId);
    public Task<(TOut, AccessoryDto?)> ConvertWithExtraAccessory(LineItem lineItem, Guid scopeId);
    public Task<(TOut, PetDto?, AccessoryDto?)> ConvertWithExtras(LineItem lineItem, Guid scopeId);
    public Task<(TOut, List<PetDto>?, List<AccessoryDto>?)> ConvertWithMultipleExtras(LineItem lineItem, Guid scopeId);
}
