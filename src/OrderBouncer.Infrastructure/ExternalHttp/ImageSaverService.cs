using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.HttpClients;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;

namespace OrderBouncer.Infrastructure.ExternalHttp;

public class ImageSaverService : IImageSaverService
{
    private readonly IConfiguration _configuration;
    private readonly IFileCleanupService _cleanupService;
    private readonly IImageFetcherService _fetcherService;
    private readonly ILogger<ImageSaverService> _logger;

    public ImageSaverService(IConfiguration configuration, IFileCleanupService cleanupService, IImageFetcherService fetcherService, ILogger<ImageSaverService> logger){
        _configuration = configuration;
        _cleanupService = cleanupService;
        _fetcherService = fetcherService;
        _logger = logger;
    }

    public async Task<string> Save(string url, string fileName, string fileExtension)
    {
        _logger.LogInformation("Saving Image from url: {0}, with fileName: {1} and extension: {2}", url, fileName, fileExtension);
        if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute)){
            throw new ArgumentException("Invalid URL provided.", nameof(url));
        }

        string? savePath = _configuration["ImageSaver:Path"];
        if(savePath is null){
            throw new ArgumentNullException("Save path for imagesaver is null, can not read properly from appsettings");
        }
        _logger.LogDebug("Save path retrieved from appsettings: {0}", savePath);

        if (!Directory.Exists(savePath)){
            _logger.LogWarning("Directory not exists in current savePath, creating new");
            Directory.CreateDirectory(savePath);
        }

        string fullPath = Path.Combine(savePath, $"{fileName}.{fileExtension}");
        _logger.LogDebug("Path combining complete, combined path: {0}", fullPath);

        try{
            await using Stream stream = await _fetcherService.FetchAsync(url);
            await using FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,4096, true);

            _logger.LogDebug("Starting to copy stream into fileStream");    
            await stream.CopyToAsync(fs);
            _logger.LogDebug("Stream is successfully copied into FileStream and it wrote image into path: {0}", fullPath);

            _cleanupService.Register(fullPath);

            return fullPath;

        } catch(Exception ex){
            _logger.LogError("Error occured while saving image\nmessage: {0}\nstackTrace: {1}", ex.Message, ex.StackTrace);
            throw;
        }
    }
}
