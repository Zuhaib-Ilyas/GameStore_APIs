using GameStore_API.Services;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register the service
builder.Services.AddScoped<IGameCharactersService, GameCharactersService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 2. Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.Seq("http://localhost:5341") // Logs Seq dashboard pe jayenge
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Default logger ko Serilog se replace karein

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
