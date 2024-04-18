using FlightManager.Core.DTO;
using FlightManager.Core.Enums;
using FlightManager.Core.Utilities;

namespace FlightManager.Core.ServiceInterfaces
{
    /// <summary>
    /// Interface for Flights service which contains business logic to manage flights
    /// </summary>
    public interface IFlightsService
    {
        /// <summary>
        /// Returns nth (based on pageNumber) 10 flights filtered and sorted based on parameters
        /// </summary>
        /// <param name="filter">Filters for departure and arrival cities</param>
        /// <param name="pageNumber">Describes which nth 10 flights will be returned</param>
        /// <param name="sortType">Specifies what flights will be sorted by (default is Departure date)</param>
        /// <param name="sortOrder">Specifies order of sorting (default is ascending)</param>
        /// <returns>Sorted and filtered list of flights as FlightResponse objects</returns>
        Task<PagedList<FlightResponse>> GetFlightsAsync(int? pageNumber = 1, SortType? sortType = SortType.DepartureDateUTC, SortOrder? sortOrder = SortOrder.ASC, string? departureCity = "", string? arrivalCity = "");

        /// <summary>
        /// Adds new flight
        /// </summary>
        /// <param name="flightPostRequest">Added flight</param>
        /// <returns>Added flight as FlightResponse object</returns>
        Task<FlightResponse> PostFlightAsync(FlightPostRequest? flightPostRequest);

        /// <summary>
        /// Updates flight
        /// </summary>
        /// <param name="flightPutRequest">Flight object with id of updated flight and its new properties</param>
        /// <returns>Updated flight as FlightResponse object</returns>
        Task<FlightResponse> PutFlightAsync(FlightPutRequest? flightPutRequest);

        /// <summary>
        /// Deletes flight
        /// </summary>
        /// <param name="flightID">ID of deleted flight</param>
        Task DeleteFlightAsync(Guid? flightID);
    }
}
