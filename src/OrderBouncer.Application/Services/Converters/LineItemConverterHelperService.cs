using System;
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
        foreach (var item in props)
        {
            string path = await _imageSaver.Save(item.Value, item.Name);
            imagePaths.Add(path);
        }

        return imagePaths;
    }
}
