using System;
using System.Security.Cryptography;
using System.Text;

namespace OrderBouncer.Web.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthenticationMiddleware> _logger;

    private const string SHOPIFY_AUTH_HEADER_NAME = "X-Shopify-Hmac-SHA256";
    public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger){
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IConfiguration configuration){
        _logger.LogInformation("AuthenticationMiddleware is starting");

        PathString path = context.Request.Path;

        if(path.StartsWithSegments("/metrics", StringComparison.OrdinalIgnoreCase)){
            _logger.LogInformation("User came for metrics, I let that sink in.");

            await _next(context);
            return;
        }

        int requestPort = context.Connection.LocalPort;
        
        if(requestPort.ToString() != configuration["ASPNET_PORT"]){
            _logger.LogDebug("Request is not going to analyzed for Shopify Webhook Secret");
            await _next(context);
            return;
        }
        
        string? shopifySecret = configuration["Shopify:WebhookSecret"];
        if(shopifySecret is null) throw new ArgumentNullException("ShopifySecret is null, can not retrieve from appsettings");

        string hmacHeader = context.Request.Headers[SHOPIFY_AUTH_HEADER_NAME].ToString();
        

        if(string.IsNullOrEmpty(hmacHeader)){
            _logger.LogError("Request missing Shopify HMAC Header");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized, missing Shopify HMAC Header");
            return;
        }

        _logger.LogDebug("HMAC successfully arrived from request");
        _logger.LogTrace("Arrived HMAC: {0}", hmacHeader);

        context.Request.EnableBuffering();
        using StreamReader reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, leaveOpen: true);
        string body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        using HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(shopifySecret));
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
        string hash = Convert.ToBase64String(computedHash);

        if(hash != hmacHeader){
            _logger.LogError("Invalid Shopify webhook secret");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized, Invalid Shopify webhook secret");
            return;
        }

        _logger.LogInformation("Shopify webhook successfully authenticated");

        await _next(context);
        return;
    }
}
