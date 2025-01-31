using System;
using System.Text.Json;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.UseCases;

public interface IOrderCreatedUseCase
{
    public Task<bool> ExecuteAsync(OrderDto orderDto, CancellationToken cancellationToken);
}
