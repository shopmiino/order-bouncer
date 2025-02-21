using System;

namespace OrderBouncer.Application.Interfaces.Infrastructure.Services;

public interface IFileCleanupService
{
    public void Register(string path);
    public void CleanupAsync();
}
