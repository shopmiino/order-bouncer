using System;
using System.Text.Json.Serialization;

namespace OrderBouncer.Application.DTOs;

public class OrderCreatedShopifyRequestWrapperDto
{
    [JsonPropertyName("order")]
    public OrderCreatedShopifyRequestDto? Order { get; set; }
}
