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
        /// <param name="filter">Filters for departure and arrival cities</param>
        /// <param name="pageNumber">Describes </param>
        /// <param name="sortType">Specifies what flights will be sorted by (default is Departure date)</param>
        /// <param name="sortOrder">Specifies order of sorting (default is ascending)</param>
        /// <returns>Sorted list of all flights</returns>
        PagedList<Flight> GetFlights(FlightFilter filter, uint pageNumber = 1, SortType sortType = SortType.DepartureDate, SortOrder sortOrder = SortOrder.ASC);

        /// <summary>
        /// Gets single flight from database
        /// </summary>
        /// <param name="flightID">ID of flight</param>
        /// <returns>Flight with given id</returns>
        Flight? GetFlightByID(Guid flightID);

        /// <summary>
        /// Adds new flight to database
        /// </summary>
        /// <param name="flight">Added flight</param>
        /// <returns>Added flight</returns>
        Flight PostFlight(Flight flight);

        /// <summary>
        /// Updates flight
        /// </summary>
        /// <param name="flight">Flight object with id of updated flight and its new properties</param>
        /// <returns>Updated flight</returns>
        Flight PutFlight(Flight flight);

        /// <summary>
        /// Deletes flight from database
        /// </summary>
        /// <param name="flightID"></param>
        void DeleteFlight(Guid flightID);
    }
}
