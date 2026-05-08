using Zyprix.Data.Interfaces;
using Zyprix.Data.Repositories;
using Zyprix.Services;
using Zyprix.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Repositories
builder.Services.AddScoped<IKlineRepository>(sp => new KlineRepository(conn));
builder.Services.AddScoped<ICoinRepository>(sp => new CoinRepository(conn));

// Services
builder.Services.AddScoped<IKlineService, KlineService>();
builder.Services.AddScoped<ICoinService, CoinService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
