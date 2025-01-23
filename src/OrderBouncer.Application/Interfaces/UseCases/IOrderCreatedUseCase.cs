using System;
using System.Text.Json;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.Interfaces.UseCases;

public interface IOrderCreatedUseCase
{
    public Task<bool> ExecuteAsync(JsonDocument json, CancellationToken cancellationToken);
}
