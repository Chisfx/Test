using Serilog;
using Test.Api.Extensions;
using Test.Api.Middlewares;
using Test.Application.Extensions;
using Test.Infrastructure.DbContexts;
using Test.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configSettings = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddApiPresentations(builder.Configuration);
builder.Services.AddContextInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await CreateDatabaseAsync(app.Services);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configSettings)
    .CreateLogger();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task CreateDatabaseAsync(IServiceProvider serviceProvider)
{
    await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}
