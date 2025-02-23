using System;

namespace OrderBouncer.Application.Interfaces.Infrastructure.Services;

public interface IFileCleanupService
{
    public void Register(Guid jobId, string path);
    public void Cleanup(Guid jobId);
}
