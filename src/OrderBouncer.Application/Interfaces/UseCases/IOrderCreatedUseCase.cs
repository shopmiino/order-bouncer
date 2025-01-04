using System;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.Interfaces.UseCases;

public interface IOrderCreatedUseCase
{
    public bool Create(string json);
}
