using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemsConverterService<TOut>
{
    public TOut Convert(LineItem lineItem);
}
