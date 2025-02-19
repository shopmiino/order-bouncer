using System;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Infrastructure.ExternalHttp;

public class ImageFetcherService : IImageFetcherService
{
    private readonly HttpClient _httpClient;

    public ImageFetcherService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ImageClient");
    }
    public async Task<Stream> FetchAsync(string url)
    {

        using HttpResponseMessage response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync();

    }
}
