using FlightManager.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace FlightManager.Infrastructure.DatabaseContext
{
    /// <summary>
    /// Custom DbContext for application
    /// </summary>
    public class AppDbContext : IdentityUserContext<AppUser, Guid>
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

            // adding seed data from wwwroot/seedData/aircraftTypes.json file to AircraftTypes table
            //builder.Entity<AircraftType>().HasData(new AircraftType()
            //{
            //    AircraftTypeID = Guid.Parse("DD0C5EAA-811A-4EBD-8952-9268C93A20B0"),
            //    Name = "Embraer"
            //});
            //builder.Entity<AircraftType>().HasData(new AircraftType()
            //{
            //    AircraftTypeID = Guid.Parse("FC2B874F-45FD-42B1-9D4A-11E9B1B0413A"),
            //    Name = "Boeing"
            //});
            //builder.Entity<AircraftType>().HasData(new AircraftType()
            //{
            //    AircraftTypeID = Guid.Parse("46754CFE-7DC0-4EBB-A67C-B8970CCEBB9B"),
            //    Name = "Airbus"
            //});
            //builder.Entity<AircraftType>().HasData(new AircraftType()
            //{
            //    AircraftTypeID = Guid.Parse("AC14FD42-9D4C-47B9-A989-4832E3843A0C"),
            //    Name = "Bombardier"
            //});

            string aircraftTypesString = System.IO.File.ReadAllText("wwwroot/seedData/aircraftTypes.json");
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
