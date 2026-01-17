var builder = WebApplication.CreateBuilder(args);

// suppress verbose SQL/query logs from Marten/Npgsql so queries aren't printed to the terminal
builder.Logging.AddFilter("Marten", Microsoft.Extensions.Logging.LogLevel.Warning);
builder.Logging.AddFilter("Npgsql", Microsoft.Extensions.Logging.LogLevel.Warning);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
})
    .UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();