using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.GoogleDrive;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Domain.Aggregates;

namespace OrderBouncer.Application.UseCases;

public class OrderCreatedUseCase : IOrderCreatedUseCase
{
    private readonly IJsonMapping<Order> _orderMapping;
    private readonly IOutboxExecutor _outbox;
    private readonly ILogger<OrderCreatedUseCase> _logger;
    public OrderCreatedUseCase(IJsonMapping<Order> orderMapping, IOutboxExecutor outbox, ILogger<OrderCreatedUseCase> logger){
        _orderMapping = orderMapping;
        _outbox = outbox;
        _logger = logger;
    }
    public async Task<bool> ExecuteAsync(JsonDocument json, CancellationToken cancellationToken)
    {   
        JsonElement element = json.RootElement.GetProperty("photo");
        string imageData = element.GetProperty("data").GetString();

        string base64Data = imageData.Substring(imageData.IndexOf(",") + 1);
        
        byte[] data = Convert.FromBase64String(base64Data);
        string fileName = element.GetProperty("fileName").GetString();
        string fileFormat = element.GetProperty("mimeType").GetString();
        
        string filePath = fileName + fileFormat;
        await File.WriteAllBytesAsync(filePath, data);

        _logger.LogInformation("Executing Json: {0}", fileName);
        //JsonNode? node = JsonNode.Parse(json);
        //_logger.LogDebug("Node tried to parse the json, sample {0}", node["mesaj"]);

        //if(node is null){
        //    _logger.LogDebug("{0} node is null", nameof(OrderCreatedUseCase));
        //    return false;
        //}
        //Order? order = await _orderMapping.Map(node);
        //DriveUploadDto dto = new ();
        
        //Save to db
        
        await _outbox.ExecutePathAsync(filePath, cancellationToken);
        return false;
    }
}
