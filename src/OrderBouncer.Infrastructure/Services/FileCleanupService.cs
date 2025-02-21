using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;

namespace OrderBouncer.Infrastructure.Services;

public class FileCleanupService : IFileCleanupService
{
    private readonly ILogger<FileCleanupService> _logger;
    private ConcurrentBag<string> _files = [];
    public FileCleanupService(ILogger<FileCleanupService> logger)
    {
        _logger = logger;
    }
    public void CleanupAsync()
    {
        foreach (string path in _files)
        {

            if (!File.Exists(path))
            {
                _logger.LogWarning("No file exists at {0}", path);
                continue;
            }

            try
            {
                File.Delete(path);
                _logger.LogDebug("File deleted at {0}", path);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting file at {0}\nMessage: {1}", path, ex.Message);
            }
        }
    }

    public void Register(string path)
    {
        _files.Add(path);
        _logger.LogInformation("File added to the cleanup registry, path: {0}", path);
    }
}
