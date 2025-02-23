using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;

namespace OrderBouncer.Infrastructure.Services;

public class FileCleanupService : IFileCleanupService
{
    private readonly ILogger<FileCleanupService> _logger;
    private ConcurrentDictionary<Guid, List<string>> _files = [];

    public FileCleanupService(ILogger<FileCleanupService> logger)
    {
        _logger = logger;
    }

    public void Cleanup(Guid jobId)
    {
        if(!_files.ContainsKey(jobId)){
            _logger.LogWarning("Cleanup dictionary does not have a member of {0} key", jobId);
            return;
        }

        var jobFiles = _files[jobId];
        _logger.LogInformation("Found {0} files that matching {1} jobId specified", jobFiles.Count(), jobId);

        foreach (var path in jobFiles)
        {
            if (!File.Exists(path))
            {
                _logger.LogWarning("No file exists at {0}", path);
                continue;
            }

            try
            {
                File.Delete(path);
                _logger.LogDebug("File deleted at {0}", path);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting file at {0}\njobId: {1}\nMessage: {2}", path, jobId, ex.Message);
            }
        }
    }

    public void Register(Guid jobId, string path)
    {
        _files.AddOrUpdate(
            jobId,
            _ => [path], 
            (_, existingList) => {
                lock (existingList){
                    existingList.Add(path);
                }
                return existingList;
            }
        );
        _logger.LogInformation("File added to the cleanup registry, path: {0}", path);
    }
}
