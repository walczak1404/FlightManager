﻿using FlightManager.Core.DTO;
using FlightManager.Core.Enums;

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
        PagedList<FlightResponse> GetFlights(FlightFilter filter, uint pageNumber = 1, SortType sortType = SortType.DepartureDate, SortOrder sortOrder = SortOrder.ASC);

        /// <summary>
        /// Adds new flight
        /// </summary>
        /// <param name="flightPostRequest">Added flight</param>
        /// <returns>Added flight as FlightResponse object</returns>
        FlightResponse PostFlight(FlightPostRequest flightPostRequest);

        /// <summary>
        /// Updates flight
        /// </summary>
        /// <param name="flightPutRequest">Flight object with id of updated flight and its new properties</param>
        /// <returns>Updated flight as FlightResponse object</returns>
        FlightResponse PutFlight(FlightPutRequest flightPutRequest);

        /// <summary>
        /// Deletes flight
        /// </summary>
        /// <param name="flightID">ID of deleted flight</param>
        void DeleteFlight(Guid flightID);
    }
}
