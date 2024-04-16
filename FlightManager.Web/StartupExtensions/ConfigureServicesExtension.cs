using FlightManager.Core.Domain.RepositoryInterfaces;
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

            return services;
        }
    }
}
