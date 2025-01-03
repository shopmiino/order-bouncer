using System;
using OrderBouncer.Domain.Entities;

namespace OrderBouncer.Domain.Interfaces.Factories;

public interface IFigureFactory
{
    public FigureEntity Create();
}
