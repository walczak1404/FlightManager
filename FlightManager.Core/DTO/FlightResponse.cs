namespace FlightManager.Core.DTO
{
    /// <summary>
    /// Flight DTO for sending flight data to client
    /// </summary>
    public class FlightResponse
    {
        public Guid FlightID { get; }

        public string Number { get; }

        public DateTime DepartureDateUTC { get; }

        public string DepartureCity { get; }

        public string ArrivalCity { get; }

        public AircraftTypeResponse? AircraftType { get; }
    }
}
