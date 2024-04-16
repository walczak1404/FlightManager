using FlightManager.Core;
using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.Enums;
using FlightManager.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Infrastructure.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly AppDbContext _db;

        public FlightsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PagedList<Flight>> GetFlightsAsync(int pageNumber, Expression<Func<Flight, bool>> filterPredicate, Expression<Func<Flight, bool>> sortPredicate, SortOrder sortOrder)
        {
            var flights = _db.Flights.Where(filterPredicate);

            if (sortOrder == SortOrder.ASC)
            {
                flights = flights.OrderBy(sortPredicate);
            } else
            {
                flights = flights.OrderByDescending(sortPredicate);
            }

            return await PagedList<Flight>.CreateAsync(flights, pageNumber);
        }

        public async Task<Flight?> GetFlightByIDAsync(Guid flightID)
        {
            return await _db.Flights.FindAsync(flightID);
        }

        public async Task<Flight> PostFlightAsync(Flight flight)
        {
            _db.Flights.Add(flight);

            await _db.SaveChangesAsync();

            return flight;
        }

        public async Task<Flight?> PutFlightAsync(Flight flight)
        {
            Flight? matchingFlight = await _db.Flights.FindAsync(flight.FlightID);

            if (matchingFlight == null) return null;

            matchingFlight.Number = flight.Number;
            matchingFlight.DepartureDateUTC = flight.DepartureDateUTC;
            matchingFlight.DepartureCity = flight.DepartureCity;
            matchingFlight.ArrivalCity = flight.ArrivalCity;
            matchingFlight.AircraftTypeID = flight.AircraftTypeID;

            await _db.SaveChangesAsync();

            return matchingFlight;
        }

        public async Task DeleteFlightAsync(Flight deletedFlight)
        {
            _db.Flights.Remove(deletedFlight);

            await _db.SaveChangesAsync();
        }
    }
}
