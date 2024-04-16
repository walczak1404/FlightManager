using FlightManager.Web.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// configure services
builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

// Configure the request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
