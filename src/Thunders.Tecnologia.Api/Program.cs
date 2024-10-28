using Serilog;
using Thunders.Tecnologia.Application.Extensions;
using Thunders.Tecnologia.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for logging

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pass IConfiguration to AddInfrastructure
builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thunders.Tecnologia.Api");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});
app.UseAuthorization();
app.MapControllers();
app.Run();