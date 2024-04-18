﻿using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.DTO;
using FlightManager.Core.Enums;
using FlightManager.Core.ServiceInterfaces;
using FlightManager.Core.Services.Helpers;
using FlightManager.Core.Utilities;
using System.Linq.Expressions;

namespace FlightManager.Core.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRepository _flightsRepository;

        public FlightsService(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<PagedList<FlightResponse>> GetFlightsAsync(int? pageNumber = 1, SortType? sortType = SortType.DepartureDateUTC, SortOrder? sortOrder = SortOrder.ASC, string? departureCity = "", string? arrivalCity = "")
        {
            if (pageNumber == null || sortType == null || sortOrder == null || departureCity == null || arrivalCity == null) throw new ArgumentNullException("Jeden z podanych parametrów jest pusty");

            if (pageNumber < 1) throw new ArgumentOutOfRangeException("Numer strony nie może być mniejszy niż 1");

            Expression<Func<Flight, bool>> filterPredicate = f => f.DepartureCity!.Contains(departureCity) && f.ArrivalCity!.Contains(arrivalCity);

            Expression<Func<Flight, object>> sortPredicate = sortType switch
            {
                SortType.DepartureCity => f => f.DepartureCity!,
                SortType.ArrivalCity => f => f.ArrivalCity!,
                SortType.AircraftType => f => f.AircraftType!.Name!,
                _ => f => f.DepartureDateUTC
            };

            PagedList<Flight> fetchedFlights = await _flightsRepository.GetFlightsAsync(pageNumber.Value, filterPredicate, sortPredicate, sortOrder.Value);

            PagedList<FlightResponse> fetchedFlightResponses = new PagedList<FlightResponse>(fetchedFlights.Items.Select(f => f.ToFlightResponse()).ToList(), fetchedFlights.Page, fetchedFlights.TotalPagesCount);

            return fetchedFlightResponses;

        }

        public async Task<FlightResponse> PostFlightAsync(FlightPostRequest? flightPostRequest)
        {
            Validation.Validate(flightPostRequest);

            Flight addedFlight = await _flightsRepository.PostFlightAsync(flightPostRequest!.ToFlight()); // flight is validated for null in Validation.Validate(flightPostRequest)

            return addedFlight.ToFlightResponse();
        }

        public async Task<FlightResponse> PutFlightAsync(FlightPutRequest? flightPutRequest)
        {
            Validation.Validate(flightPutRequest);

            Flight? flightFromDatabase = await _flightsRepository.GetFlightByIDAsync(flightPutRequest!.FlightID); // flight is validated for null in Validation.Validate(flightPutRequest)

            if (flightFromDatabase == null) throw new ArgumentException("Nie znaleziono takiego lotu");

            Flight updatedFlight = await _flightsRepository.PutFlightAsync(flightPutRequest);

            return updatedFlight.ToFlightResponse();
        }

        public async Task DeleteFlightAsync(Guid? flightID)
        {
            if (flightID == null) throw new ArgumentNullException("Nie podano ID lotu");

            Flight? flightFromID = await _flightsRepository.GetFlightByIDAsync(flightID.Value);

            if (flightFromID == null) throw new ArgumentException("Podano niewłaściwe ID");

            await _flightsRepository.DeleteFlightAsync(flightFromID);
        }
    }
}
