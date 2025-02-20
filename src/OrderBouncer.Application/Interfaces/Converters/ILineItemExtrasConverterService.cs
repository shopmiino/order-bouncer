using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemExtrasConverterService
{
    public Task<BaseDto> ConvertExtra(LineItem lineItem, IList<NoteAttribute[]> props, Func<NoteAttribute[], NoteAttribute[]?> noteGetter, int position = 0);
}
