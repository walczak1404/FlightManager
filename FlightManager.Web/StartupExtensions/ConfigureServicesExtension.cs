using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.ServiceInterfaces;
using FlightManager.Core.Services;
using FlightManager.Infrastructure.DatabaseContext;
using FlightManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            // add database
            services.AddDbContext<AppDbContext>(options =>
            {
                // connection string stored in user secrets
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // add repositories to IoC container
            services.AddScoped<IFlightsRepository, FlightsRepository>();
            services.AddScoped<IAircraftTypesRepository, IAircraftTypesRepository>();

            // add services to IoC container
            services.AddScoped<IAircraftTypesService, AircraftTypesService>();

            return services;
        }
    }
}
