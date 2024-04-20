using FlightManager.Web.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// configure services
builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
