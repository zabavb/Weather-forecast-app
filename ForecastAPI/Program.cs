using ForecastAPI.Models;
using ForecastAPI.Repositories;
using ForecastAPI.Services;
using Microsoft.OpenApi.Models;
using Refit;
using Serilog;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRefitClient<IForecastApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ForecastApi:BaseUrl"]!));

builder.Services.Configure<ForecastApiSettings>(builder.Configuration.GetSection("ForecastApi"));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var redisConfig = config.GetSection("Redis");

    var options = new ConfigurationOptions
    {
        EndPoints = { { redisConfig["Host"]!, int.Parse(redisConfig["Port"]!) } },
        User = redisConfig["User"],
        Password = redisConfig["Password"]
    };

    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddScoped<IForecastRepository, ForecastRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ForecastAPI",
        Version = "v1"
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var logFilePath = Path.Combine(AppContext.BaseDirectory, "logs.log");
Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);

Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("LogTime", DateTime.UtcNow)
    .WriteTo.Console(outputTemplate: "[{Level:u3}]: {Message:lj} - {LogTime:yyyy-MM-dd HH:mm:ss}{NewLine}{NewLine}")
    .WriteTo.File(
        logFilePath,
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        outputTemplate:
        "[{Level:u3}]: {Message:lj} | Exception: {Exception} - {Timestamp:yyyy-MM-dd HH:mm:ss}{NewLine}{NewLine}"
    )
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:53506")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForecastAPI"); });
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
