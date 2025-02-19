using System;

namespace OrderBouncer.Application.Interfaces.HttpClients;

public interface IImageFetcherService
{
    public Task<Stream> FetchAsync(string url);
}
