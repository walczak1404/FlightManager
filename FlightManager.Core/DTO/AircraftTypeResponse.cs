namespace FlightManager.Core.DTO
{
    /// <summary>
    /// AircraftType DTO for sending data of aircraft type to client
    /// </summary>
    public class AircraftTypeResponse
    {
        public Guid AircraftTypeID { get; set; }

        public string Name { get; set; }
    }
}
