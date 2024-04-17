using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Enums;
using FlightManager.Core.Utilities;
using System.Linq.Expressions;

namespace FlightManager.Core.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Flights repository interface for managing flights database operations
    /// </summary>
    public interface IFlightsRepository
    {
        /// <summary>
        /// Gets nth (based on pageNumber) 10 flights from database filtered and sorted based on parameters
        /// </summary>
        /// <param name="pageNumber">Describes which nth 10 flights will be returned</param>
        /// <param name="filterPredicate">Predicate to filter departure and arrival cities</param>
        /// <param name="sortPredicate">Predicate to sort flights</param>
        /// <param name="sortOrder">Defines in what order should flights be sorted</param>
        /// <returns>Sorted and filtered list of flights</returns>
        Task<PagedList<Flight>> GetFlightsAsync(int pageNumber, Expression<Func<Flight, bool>> filterPredicate, Expression<Func<Flight, bool>> sortPredicate, SortOrder sortOrder);

        /// <summary>
        /// Gets single flight from database
        /// </summary>
        /// <param name="flightID">ID of wanted flight</param>
        /// <returns>Flight with given id</returns>
        Task<Flight?> GetFlightByIDAsync(Guid flightID);

        /// <summary>
        /// Adds new flight to database
        /// </summary>
        /// <param name="flight">Added flight</param>
        /// <returns>Added flight</returns>
        Task<Flight> PostFlightAsync(Flight flight);

        /// <summary>
        /// Updates flight in database
        /// </summary>
        /// <param name="flight">Flight object with id of updated flight and its new properties</param>
        /// <returns>Updated flight</returns>
        Task<Flight?> PutFlightAsync(Flight flight);

        /// <summary>
        /// Deletes flight from database
        /// </summary>
        /// <param name="deletedFlight">Flight which will be deleted</param>
        Task DeleteFlightAsync(Flight deletedFlight);
    }
}
