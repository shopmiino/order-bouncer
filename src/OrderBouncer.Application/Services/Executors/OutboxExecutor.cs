using System;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.OutboxPublisher;

namespace OrderBouncer.Application.Services.Executors;

public class OutboxExecutor : IOutboxExecutor
{
    private readonly IEnumerable<IOutboxPublisher> _publishers;

    public OutboxExecutor(IEnumerable<IOutboxPublisher> publishers){
        _publishers = publishers;
    }
    public async Task ExecuteAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        foreach (IOutboxPublisher publisher in _publishers){
            try {
                await publisher.PublishAsync(fileContent, cancellationToken);
            } catch (Exception ex) {
                //Log exception
            }
        }
    }
}
