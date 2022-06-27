using XamRealmDb.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<DataService>();
builder.Services.AddHostedService<DataUpdaterWorker>();

builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.MapGet("latest", (DataService dataService) => dataService.Values);
app.MapPost("latest", (string source, DataService dataService) => dataService.UpdateValue(source));

app.Run();
