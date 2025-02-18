using System;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Domain.Factories;

public class FigureFactory : IFactory<FigureCreateDto, FigureEntity>
{
    public FigureEntity Create(FigureCreateDto? dto)
    {
        throw new NotImplementedException();
    }
}
