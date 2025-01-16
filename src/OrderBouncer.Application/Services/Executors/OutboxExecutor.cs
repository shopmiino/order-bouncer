using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.OutboxPublisher;

namespace OrderBouncer.Application.Services.Executors;

public class OutboxExecutor : IOutboxExecutor
{
    private readonly IEnumerable<IOutboxPublisher> _publishers;
    private readonly ILogger<OutboxExecutor> _logger;

    public OutboxExecutor(IEnumerable<IOutboxPublisher> publishers, ILogger<OutboxExecutor> logger){
        _publishers = publishers;
        _logger = logger;
    }
    public async Task ExecuteBytesAsync(byte[] fileContent, CancellationToken cancellationToken)
    {
        foreach (IOutboxPublisher publisher in _publishers){
            try {
                _logger.LogInformation("Trying to execute {0} publisher", publisher.TargetSystem);
                await publisher.PublishBytesAsync(fileContent, cancellationToken);
            } catch (Exception ex) {
                _logger.LogError("An error occured while publishing {0}, Message: {1}", publisher.TargetSystem, ex.Message);
            }
        }
    }


    public async Task ExecutePathAsync(string filePath, CancellationToken cancellationToken)
    {
        foreach (IOutboxPublisher publisher in _publishers){
            try {
                _logger.LogInformation("Trying to execute {0} publisher", publisher.TargetSystem);
                await publisher.PublishPathAsync(filePath, cancellationToken);
            } catch (Exception ex) {
                _logger.LogError("An error occured while publishing {0}, Message: {1}", publisher.TargetSystem, ex.Message);
            }
        }
    }
}
