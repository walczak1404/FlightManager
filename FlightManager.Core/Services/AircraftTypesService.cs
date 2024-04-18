using FlightManager.Core.Domain.Entities;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.DTO;
using FlightManager.Core.ServiceInterfaces;

namespace FlightManager.Core.Services
{
    public class AircraftTypesService : IAircraftTypesService
    {
        private readonly IAircraftTypesRepository _aircraftTypesRepository;

        public AircraftTypesService(IAircraftTypesRepository aircraftTypes)
        {
            _aircraftTypesRepository = aircraftTypes;
        }

        public async Task<List<AircraftTypeResponse>> GetAllAircraftTypesAsync()
        {
            List<AircraftType> retrievedTypes = await _aircraftTypesRepository.GetAllAircraftTypesAsync();

            return retrievedTypes.Select(type => type.ToAircraftTypeResponse()).ToList();
        }
    }
}
