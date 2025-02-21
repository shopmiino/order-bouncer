using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemConverterHelperService : ILineItemConverterHelperService
{
    private readonly IImageSaverService _imageSaver;
    private readonly ILogger<LineItemConverterHelperService> _logger;
    public LineItemConverterHelperService(IImageSaverService imageSaver, ILogger<LineItemConverterHelperService> logger)
    {
        _imageSaver = imageSaver;
        _logger = logger;
    }

    public async Task<ICollection<string>> BatchImageSaveAndAdd(NoteAttribute[] props, ICollection<string> imagePaths)
    {
        _logger.LogInformation("BarchImageSaveAndAdd is starting with imagePath count: {0}", imagePaths.Count());
        var tempList = imagePaths.ToList();


        var tasks = props.Select(async p => {
                string extension = p.Value.Split(".").Last();
                _logger.LogDebug("Selected extension for {0}, {1}, is: {2}", p.Value, p.Name, extension);

                string path = await _imageSaver.Save(p.Value, p.Name, extension);
                return path;
        });
        
        _logger.LogDebug("{0} tasks are grouped and starting to run in parallel with Task.WhenAll", tasks.Count());
        string[] results = await Task.WhenAll(tasks);

        tempList.AddRange(results);

        _logger.LogDebug("Results added to tempImagePaths, current path count is {0}. Returning the tempPaths.", tempList.Count());
        return tempList;
    }
}