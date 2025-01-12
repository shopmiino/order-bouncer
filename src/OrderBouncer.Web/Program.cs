using OrderBouncer.Web;
using OrderBouncer.GoogleDrive;
using OrderBouncer.GoogleSheets;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureSerilog();


builder.Services.AddGoogleDrive();
builder.Services.AddGoogleSheets();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
