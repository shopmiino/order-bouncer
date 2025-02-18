using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class AccessoryDtoLineItemConverterService : ILineItemsConverterService<AccessoryDto>
{
    public AccessoryDto Convert(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
}
