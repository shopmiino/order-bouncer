using System;
using System.Threading.Channels;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Buffer;

public interface ICreateRequestBufferService
{
    public ChannelReader<OrderDto> Reader {get;}
    public Task EnqueueAsync(OrderDto orderDto, CancellationToken cancellationToken);

}
