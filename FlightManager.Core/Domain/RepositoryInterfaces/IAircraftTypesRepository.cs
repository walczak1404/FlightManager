using FlightManager.Core.Domain.Entities;

namespace FlightManager.Core.Domain.RepositoryInterfaces
{
    /// <summary>
    /// AircraftTypes repository interface for retrieving Aircraft types
    /// </summary>
    public interface IAircraftTypesRepository
    {
        /// <summary>
        /// Gets all aircraft types stored in database
        /// </summary>
        /// <returns></returns>
        List<AircraftType> GetAircraftTypes();
    }
}
