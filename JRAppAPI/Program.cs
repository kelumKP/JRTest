using JRAppAPI;
using JRAppAPI.Infrastructure.HttpClient;
using JRAppAPI.Repositories;
using JRAppAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register IHttpClientFactory
builder.Services.AddHttpClient();

// In Startup.cs, within the ConfigureServices method:
builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

// Add the following line to register ListingService
builder.Services.AddScoped<IListingService, ListingService>();
// Register ListingsRepository as IListingsRepository
builder.Services.AddScoped<IListingsRepository, ListingsRepository>();

// Configure AppSettings to read from appsettings.json
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

