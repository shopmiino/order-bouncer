using System;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;

namespace OrderBouncer.Infrastructure;

public static class ImageClientPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(){
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromMilliseconds(400));
    }

    public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(){
        return Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger){
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (outcome, breakDelay) => {
                    logger.LogWarning("Circuit is OPEN! Breaking for {0} seconds.", breakDelay.TotalSeconds);
                },
                onReset: () => {
                    logger.LogInformation("Circuit is CLOSED! Normal operation resumed.");
                },
                onHalfOpen: () => {
                    logger.LogWarning("Circuit is HALF-OPEN! Testing the service.");
                }
            );
    }
}
