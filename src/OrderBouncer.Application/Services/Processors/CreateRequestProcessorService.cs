using System;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Processors;

public class CreateRequestProcessorService : ICreateRequestProcessorService
{
    public Task ProcessAsync(OrderDto orderDto)
    {
        //process
        return null;
    }
}
