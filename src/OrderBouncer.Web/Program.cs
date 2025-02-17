using OrderBouncer.Web;
using OrderBouncer.GoogleDrive;
using OrderBouncer.GoogleSheets;
using OrderBouncer.Application;
using Hangfire;
using Hangfire.SQLite;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureSerilog();

//builder.Services.AddGoogleDrive();
builder.Services.AddGoogleSheets();

//Application Layer
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.ConfigureHangfireDashboard();

app.Run();
