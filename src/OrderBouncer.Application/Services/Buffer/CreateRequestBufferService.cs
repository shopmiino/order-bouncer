using System;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Buffer;

public class CreateRequestBufferService : ICreateRequestBufferService
{
    private readonly Channel<OrderDto> _channel;
    private readonly ILogger<CreateRequestBufferService> _logger;
    private const int CHANNEL_CAPACITY = 1000;
    public CreateRequestBufferService(ILogger<CreateRequestBufferService> logger){
        _logger = logger;

        _logger.LogDebug("Creating Bounded Channel with capacity of {0}", CHANNEL_CAPACITY);
        _channel = Channel.CreateBounded<OrderDto>(new BoundedChannelOptions(CHANNEL_CAPACITY)
        {
            SingleReader = true,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.Wait,
        });
    }

    public async Task EnqueueAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Adding OrderDto into the queue");
        await _channel.Writer.WriteAsync(orderDto, cancellationToken);
    }

    public ChannelReader<OrderDto> Reader => _channel.Reader;
}
