using FlightManager.Core.DTO;

namespace FlightManager.Core.ServiceInterfaces
{
    /// <summary>
    /// Interface for AircraftTypes service which contains business logic to manage aircraft types
    /// </summary>
    public interface IAircraftTypesService
    {
        /// <summary>
        /// Gets all aircraft types
        /// </summary>
        /// <returns>List of aircraft types as AircraftTypeResponse objects</returns>
        List<AircraftTypeResponse> GetAllAircraftTypes();
    }
}
