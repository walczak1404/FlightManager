using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.DTO;
using FlightManager.Core.Enums;
using FlightManager.Core.ServiceInterfaces;
using FlightManager.Core.Services.Helpers;
using FlightManager.Core.Utilities;

namespace FlightManager.Core.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightsRepository _flightsRepository;

        public FlightsService(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<PagedList<FlightResponse>> GetFlightsAsync(FlightFilter? filter, int pageNumber = 1, SortType sortType = SortType.DepartureDate, SortOrder sortOrder = SortOrder.ASC)
        {
            if (pageNumber < 1) throw new ArgumentOutOfRangeException("Numer strony nie może być mniejszy niż 1");

        }

        public async Task<FlightResponse> PostFlightAsync(FlightPostRequest flightPostRequest)
        {
            Validation.Validate(flightPostRequest);

            Flight addedFlight = await _flightsRepository.PostFlightAsync(flightPostRequest.ToFlight());

            return addedFlight.ToFlightResponse();
        }

        public async Task<FlightResponse> PutFlightAsync(FlightPutRequest flightPutRequest)
        {
            Validation.Validate(flightPutRequest);

            Flight? flightFromDatabase = await _flightsRepository.GetFlightByIDAsync(flightPutRequest.FlightID!.Value);

            if (flightFromDatabase == null) throw new ArgumentException("Nie znaleziono takiego lotu");

            Flight updatedFlight = await _flightsRepository.PutFlightAsync(flightPutRequest);

            return updatedFlight.ToFlightResponse();
        }

        public async Task DeleteFlightAsync(Guid flightID)
        {
            Flight? flightFromID = await _flightsRepository.GetFlightByIDAsync(flightID);

            if (flightFromID == null) throw new ArgumentException("Podano niewłaściwe ID");

            await _flightsRepository.DeleteFlightAsync(flightFromID);
        }
    }
}
