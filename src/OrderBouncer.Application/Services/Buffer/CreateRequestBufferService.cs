using System;
using System.Threading.Channels;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Services.Buffer;

public class CreateRequestBufferService : ICreateRequestBufferService
{
    private readonly Channel<OrderDto> _channel;

    public CreateRequestBufferService(){
        _channel = Channel.CreateBounded<OrderDto>(new BoundedChannelOptions(1000)
        {
            SingleReader = false,
            SingleWriter = false,
            FullMode = BoundedChannelFullMode.Wait,
        });
    }

    public async Task EnqueueAsync(OrderDto orderDto, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(orderDto, cancellationToken);
    }

    public ChannelReader<OrderDto> Reader => _channel.Reader;
}
