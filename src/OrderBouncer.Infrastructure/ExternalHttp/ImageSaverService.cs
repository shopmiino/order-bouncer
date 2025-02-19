using System;
using Microsoft.Extensions.Configuration;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Infrastructure.ExternalHttp;

public class ImageSaverService : IImageSaverService
{
    private readonly IConfiguration _configuration;

    public ImageSaverService(IConfiguration configuration){
        _configuration = configuration;
    }

    public async Task<string> Save(string url, string fileName, string? fileExtension = null)
    {
        string? savePath = _configuration["ImageSaver:Path"];
        if(savePath is null){
            throw new ArgumentNullException("Save path for imagesaver is null, can not read properly from appsettings");
        }
        
        return "name.jpg";
    }
}
