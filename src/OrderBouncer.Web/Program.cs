using OrderBouncer.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureSerilog();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
