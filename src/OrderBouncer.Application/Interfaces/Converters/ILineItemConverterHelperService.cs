using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemConverterHelperService
{
    public Task<List<string>> BatchImageSaveAndAdd(NoteAttribute[] props, List<string> imagePaths, Guid jobId);
}
