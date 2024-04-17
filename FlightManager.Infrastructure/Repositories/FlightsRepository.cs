using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.Enums;
using FlightManager.Core.Utilities;
using FlightManager.Infrastructure.DatabaseContext;
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

        public async Task<PagedList<Flight>> GetFlightsAsync(int pageNumber, Expression<Func<Flight, bool>> filterPredicate, Expression<Func<Flight, bool>> sortPredicate, SortOrder sortOrder)
        {
            // filter flights
            var flights = _db.Flights.Where(filterPredicate);

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

        public async Task<Flight?> PutFlightAsync(Flight flight)
        {
            // check if flight exists
            Flight? matchingFlight = await _db.Flights.FindAsync(flight.FlightID);

            // return null if flight does not exist
            if (matchingFlight == null) return null;

            // alter flight's properties
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
