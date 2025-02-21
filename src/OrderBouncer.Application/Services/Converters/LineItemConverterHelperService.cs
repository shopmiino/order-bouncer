using System;
using System.Collections.Concurrent;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Application.Services.Converters;

public class LineItemConverterHelperService : ILineItemConverterHelperService
{
    private readonly IImageSaverService _imageSaver;
    public LineItemConverterHelperService(IImageSaverService imageSaver)
    {
        _imageSaver = imageSaver;
    }

    public async Task<ICollection<string>> BatchImageSaveAndAdd(NoteAttribute[] props, ICollection<string> imagePaths)
    {
        var tempList = imagePaths.ToList();

        var tasks = props.Select(async p => {
                string extension = p.Value.Split(".").Last();
                string path = await _imageSaver.Save(p.Value, p.Name, extension);
                return path;
        });
        
        string[] results = await Task.WhenAll(tasks);

        tempList.AddRange(results);

        return tempList;
    }
}