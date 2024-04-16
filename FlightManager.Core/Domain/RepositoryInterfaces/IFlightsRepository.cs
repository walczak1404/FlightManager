using FlightManager.Core.Domain.Entities;
using FlightManager.Core.DTO;
using FlightManager.Core.Enums;

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
        /// <param name="predicate">Predicate filtering departure and arrival cities</param>
        /// <param name="pageNumber">Describes which nth 10 flights will be returned</param>
        /// <param name="sortType">Specifies what flights will be sorted by</param>
        /// <param name="sortOrder">Specifies order of sorting</param>
        /// <returns>Sorted and filtered list of flights</returns>
        PagedList<Flight> GetFlights(Func<Flight, bool> predicate, uint pageNumber, SortType sortType, SortOrder sortOrder);

        /// <summary>
        /// Adds new flight to database
        /// </summary>
        /// <param name="flight">Added flight</param>
        /// <returns>Added flight</returns>
        Flight PostFlight(Flight flight);

        /// <summary>
        /// Updates flight in database
        /// </summary>
        /// <param name="flight">Flight object with id of updated flight and its new properties</param>
        /// <returns>Updated flight</returns>
        Flight PutFlight(Flight flight);

        /// <summary>
        /// Deletes flight from database
        /// </summary>
        /// <param name="flightID">ID of deleted flight</param>
        void DeleteFlight(Guid flightID);
    }
}
