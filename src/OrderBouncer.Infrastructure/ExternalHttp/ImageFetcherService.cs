using System;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Infrastructure.ExternalHttp;

public class ImageFetcherService : IImageFetcherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ImageFetcherService> _logger;

    public ImageFetcherService(IHttpClientFactory httpClientFactory, ILogger<ImageFetcherService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("ImageClient");
        _logger = logger;
    }
    public async Task<Stream> FetchAsync(string url)
    {
        _logger.LogInformation("Fetching Image from url: {0}", url);

        using HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        _logger.LogTrace("Initializing new MemoryStream");
        MemoryStream ms = new();
        _logger.LogTrace("Copying content into the MemoryStream");
        await response.Content.CopyToAsync(ms);
        ms.Position = 0;

        _logger.LogDebug("Response successfully got from url: {0}, returning Stream as MemoryStream", url);
        return ms;
    }
}
