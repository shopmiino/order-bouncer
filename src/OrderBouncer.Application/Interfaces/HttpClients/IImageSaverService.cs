using System;

namespace OrderBouncer.Application.Interfaces.HttpClients;

public interface IImageSaverService
{
    public Task<string> Save(string url, string fileName, string? fileExtension = null);
}
