using FlightManager.Core.Domain.Entities;

namespace FlightManager.Core.DTO
{
    /// <summary>
    /// AircraftType DTO for sending data of aircraft type to client
    /// </summary>
    public class AircraftTypeResponse
    {
        public Guid AircraftTypeID { get; set; }

        public string? Name { get; set; }
    }

    public static class AircraftTypeExtensions
    {
        public static AircraftTypeResponse ToAircraftTypeResponse(this AircraftType aircraftType)
        {
            return new()
            {
                AircraftTypeID = aircraftType.AircraftTypeID,
                Name = aircraftType.Name
            };
        }
    }
}
