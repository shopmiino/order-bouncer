using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemExtrasConverterService
{
    public Task<BaseDto> ConvertExtra(Guid scopeId, LineItem lineItem, IList<NoteAttribute[]> props, Func<NoteAttribute[], NoteAttribute[]?> noteGetter, int position = 0, int notePosition = 0, bool hasNoImage = false);
}
