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
        _logger.LogInformation("BatchImageSaveAndAdd is starting with imagePath count: {0}, property count: {1}", imagePaths.Count(), props.Count());
        char[] splitters = ['.','/'];

        var tempList = imagePaths.ToList();
        ConcurrentBag<string> results = [];

        await Parallel.ForEachAsync(props, async (p, _) => {
            string[] splitted = p.Value.Split(splitters);
            string extension = splitted[^1];
            string name = splitted[^2]; 
            _logger.LogDebug("Selected extension for {0}, {1}, is: {2} and the name is: {3}", p.Value, p.Name, extension, name);

            string path = await _imageSaver.Save(p.Value, name, extension);
            results.Add(path);
        });
        /*
        var tasks = props.Select(async p => {
                string extension = p.Value.Split(".").Last();
                _logger.LogDebug("Selected extension for {0}, {1}, is: {2}", p.Value, p.Name, extension);

                string path = await _imageSaver.Save(p.Value, p.Name, extension);
                return path;
        }).ToList();
        */
        //_logger.LogDebug("{0} tasks are grouped and starting to run in parallel with Task.WhenAll", tasks.Count());
        //string[] results = await Task.WhenAll(tasks);

        tempList.AddRange(results);

        _logger.LogDebug("Results added to tempImagePaths, current path count is {0}. Returning the tempPaths.", tempList.Count());
        return tempList;
    }
}