using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsBaseConverterService
{
    public Task<BaseDto> GenericConvert(LineItem lineItem, Func<NoteAttribute[], NoteAttribute[]?> noteGetter);
}
