using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.ServiceInterfaces;
using FlightManager.Core.Services;
using FlightManager.Infrastructure.DatabaseContext;
using FlightManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FlightManager.Web.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            });

            // add database
            services.AddDbContext<AppDbContext>(options =>
            {
                // connection string stored in user secrets
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // add swagger
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
            });

            // add cors
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                    //.WithOrigins()
                    .WithHeaders("origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                // small password requirements to make demonstration simple
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddUserStore<UserStore<AppUser, IdentityRole<Guid>, AppDbContext, Guid>>();

            //JWT
            //services.AddAuthentication(options => {
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            // .AddJwtBearer(options => {
            //     options.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //         ValidateAudience = true,
            //         ValidAudience = configuration["Jwt:Audience"],
            //         ValidateIssuer = true,
            //         ValidIssuer = configuration["Jwt:Issuer"],
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            //     };
            // });

            services.AddAuthorization();

            // add repositories
            services.AddScoped<IFlightsRepository, FlightsRepository>();
            services.AddScoped<IAircraftTypesRepository, AircraftTypesRepository>();

            // add services
            //services.AddScoped<> JWTSERVICE
            services.AddScoped<IAircraftTypesService, AircraftTypesService>();
            services.AddScoped<IFlightsService, FlightsService>();

            return services;
        }
    }
}
