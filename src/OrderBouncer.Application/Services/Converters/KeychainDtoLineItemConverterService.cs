using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class KeychainDtoLineItemConverterService : ILineItemsConverterService<KeychainDto>
{
    public KeychainDto Convert(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
}
