using System;
using OrderBouncer.Application.Interfaces.Infrastructure.Services;

namespace OrderBouncer.Web.Middlewares;

public class FileCleanupMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<FileCleanupMiddleware> _logger;

    public FileCleanupMiddleware(RequestDelegate next, ILogger<FileCleanupMiddleware> logger){
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IFileCleanupService cleanupService){
        _logger.LogDebug("Entered the FileCleanupMiddleware");

        await _next(context);

        _logger.LogDebug("Cleanup service is STARTING");
        //cleanupService.CleanupAsync();
        _logger.LogInformation("Cleanup service is FINISHED");

        _logger.LogDebug("Leaving the FileCleanupMiddleware");
    }
}
