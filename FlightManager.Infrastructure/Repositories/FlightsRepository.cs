using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.DTO;
using FlightManager.Core.Enums;
using FlightManager.Core.Utilities;
using FlightManager.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlightManager.Infrastructure.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly AppDbContext _db;

        public FlightsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<PagedList<Flight>> GetFlightsAsync(int pageNumber, Expression<Func<Flight, bool>> filterPredicate, Expression<Func<Flight, object>> sortPredicate, SortOrder sortOrder)
        {
            // filter flights
            var flights = _db.Flights.Include(f => f.AircraftType).Where(filterPredicate);

            // sort flights based on sort order
            if (sortOrder == SortOrder.ASC)
            {
                flights = flights.OrderBy(sortPredicate);
            } else
            {
                flights = flights.OrderByDescending(sortPredicate);
            }

            // return paged list which contains flight, pageNumber, and if there are next or previous page
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

        public async Task<Flight> PutFlightAsync(FlightPutRequest updates)
        {
            // retrieve flight to update
            Flight? matchingFlight = await _db.Flights.FindAsync(updates.FlightID);

            if(matchingFlight == null)
            {
                throw new ArgumentException("Nie znaleziono lotu");
            }

            // alter flight's properties
            matchingFlight.Number = updates.Number;
            matchingFlight.DepartureDateUTC = updates.DepartureDateUTC;
            matchingFlight.DepartureCity = updates.DepartureCity;
            matchingFlight.ArrivalCity = updates.ArrivalCity;
            matchingFlight.AircraftTypeID = updates.AircraftTypeID;

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
