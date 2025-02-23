using System;
using System.Threading.Channels;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application.Interfaces.Buffer;

public interface ICreateRequestBufferService
{
    public ChannelReader<OrderCreatedShopifyRequestDto> Reader {get;}
    public Task EnqueueAsync(OrderCreatedShopifyRequestDto orderDto, CancellationToken cancellationToken);

}
