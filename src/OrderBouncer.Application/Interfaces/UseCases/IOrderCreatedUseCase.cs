using System;
using System.Text.Json;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.UseCases;

public interface IOrderCreatedUseCase
{
    public Task<bool> ExecuteAsync(OrderCreatedShopifyRequestDto requestDto, CancellationToken cancellationToken);
}
