using MusicLife.Api.Middlewares;
using MusicLife.Application;
using MusicLife.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseExceptionMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
