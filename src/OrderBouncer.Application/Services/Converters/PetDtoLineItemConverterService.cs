using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Converters;

public class PetDtoLineItemConverterService : ILineItemsConverterService<PetDto>
{
    public PetDto Convert(LineItem lineItem)
    {
        throw new NotImplementedException();
    }
}
