using OrderBouncer.Web;
using OrderBouncer.GoogleDrive;
using OrderBouncer.Infrastructure;
using OrderBouncer.GoogleSheets;
using OrderBouncer.Application;
using OrderBouncer.Application.Options;
using OrderBouncer.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureSerilog();

builder.Services.AddGoogleDrive();
builder.Services.AddGoogleSheets();

//Application Layer
builder.Services.AddApplication();

var tempProvider = builder.Services.BuildServiceProvider();
var loggerFactory = tempProvider.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("ImageHttpClient");

builder.Services.AddInfrastructure(builder.Configuration)
    .AddImageHttpClient(logger);


builder.Services.AddControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.Configure<ShopifySettings>(builder.Configuration.GetSection("Shopify"));
builder.Services.Configure<ExtractorSettings>(builder.Configuration.GetSection("PropertyExtractor"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseMiddleware<FileCleanupMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.ConfigureHangfireDashboard();

app.Run();
