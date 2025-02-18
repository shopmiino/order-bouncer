using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application.Services.Factories.DtoFactories;

public class FigureDtoFactory : IFactory<LineItem, FigureDto>
{
    public FigureDto Create(LineItem? dto)
    {
        throw new NotImplementedException();
    }
}
