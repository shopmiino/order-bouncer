using System;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.Interfaces.UseCases;

public interface IOrderCreatedUseCase
{
    public Task<bool> ExecuteAsync(string json, CancellationToken cancellationToken);
}
