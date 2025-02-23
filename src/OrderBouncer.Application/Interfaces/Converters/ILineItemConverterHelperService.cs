using System;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.HttpClients;

namespace OrderBouncer.Application.Interfaces.Converters;

public interface ILineItemConverterHelperService
{
    public Task<ICollection<string>> BatchImageSaveAndAdd(NoteAttribute[] props, ICollection<string> imagePaths, Guid jobId);
}
