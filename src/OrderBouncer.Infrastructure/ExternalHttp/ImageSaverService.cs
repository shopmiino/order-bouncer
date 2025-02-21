using System;
using Microsoft.Extensions.Configuration;
using OrderBouncer.Application.Interfaces.HttpClients;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;

namespace OrderBouncer.Infrastructure.ExternalHttp;

public class ImageSaverService : IImageSaverService
{
    private readonly IConfiguration _configuration;
    private readonly IFileCleanupService _cleanupService;
    private readonly IImageFetcherService _fetcherService;

    public ImageSaverService(IConfiguration configuration, IFileCleanupService cleanupService, IImageFetcherService fetcherService){
        _configuration = configuration;
        _cleanupService = cleanupService;
        _fetcherService = fetcherService;
    }

    public async Task<string> Save(string url, string fileName, string fileExtension)
    {
        if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute)){
            throw new ArgumentException("Invalid URL provided.", nameof(url));
        }

        string? savePath = _configuration["ImageSaver:Path"];
        if(savePath is null){
            throw new ArgumentNullException("Save path for imagesaver is null, can not read properly from appsettings");
        }

        if (!Directory.Exists(savePath)){
            Directory.CreateDirectory(savePath);
        }

        string fullPath = Path.Combine(savePath, $"{fileName}.{fileExtension}");
        
        try{
            await using Stream stream = await _fetcherService.FetchAsync(url);
            await using FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,4096, true);
            
            await stream.CopyToAsync(fs);

            _cleanupService.Register(fullPath);

            return fullPath;

        } catch(Exception ex){
            //write logging
            throw;
        }
    }
}
