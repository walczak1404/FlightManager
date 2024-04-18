using FlightManager.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FlightManager.Infrastructure.DatabaseContext
{
    /// <summary>
    /// Custom DbContext for application
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        // creating Flights table based on Flight model class
        public virtual DbSet<Flight> Flights { get; set; }

        // creating AircraftTypes table based on AircraftType model class
        public virtual DbSet<AircraftType> AircraftTypes { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // configuring database to automatically generate FlightIDs when new row is inserted into Flights table
            builder.Entity<Flight>().Property(f => f.FlightID).HasDefaultValueSql("NEWSEQUENTIALID()");

            // adding seed data from SeedData/aircraftTypes.json file to AircraftTypes table
            string aircraftTypesString = System.IO.File.ReadAllText("SeedData/aircraftTypes.json");
            List<AircraftType>? aircraftTypesList = System.Text.Json.JsonSerializer.Deserialize<List<AircraftType>>(aircraftTypesString);

            if (aircraftTypesList != null)
            {
                foreach(AircraftType aircraftType in aircraftTypesList)
                {
                    builder.Entity<AircraftType>().HasData(aircraftType);
                }
            }

        }
    }
}
