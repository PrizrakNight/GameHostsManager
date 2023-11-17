using GameHostsManager.WebApi.SwaggerDoc.OperationFilters;
using GameHostsManager.Infrastructure.IoC;
using GameHostsManager.WebApi.Middlewares;
using GameHostsManager.WebApi.CoravelInvocables;
using GameHostsManager.WebApi.Extensions;
using Coravel;
using GameHostsManager.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<UserIdentityHeaderFilter>();

    var filePaths = new[]
    {
        // There are hardcore paths here, but for the current solution it's not scary
        Path.Combine(AppContext.BaseDirectory, "GameHostsManager.Application.xml"),
        Path.Combine(AppContext.BaseDirectory, "GameHostsManager.WebApi.xml")
    };

    foreach (var filePath in filePaths)
    {
        c.IncludeXmlComments(filePath);
    }
});

builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddMappings(builder.Configuration);
builder.Services.AddPersistanceMemoryCache(builder.Configuration);
builder.Services.AddFluentValidation(builder.Configuration);

var app = builder.Build();

app.Services.UseScheduler(scheduler =>
{
    var cleanupOptions = app.Configuration
        .GetSection(nameof(HostRoomCleanupOptions))
        .Get<HostRoomCleanupOptions>();

    scheduler.Schedule<HostRoomsCleanupInvocable>()
        .EverySeconds(cleanupOptions!.CleanupPeriodInSeconds);
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

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
