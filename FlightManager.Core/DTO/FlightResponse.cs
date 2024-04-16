namespace FlightManager.Core.DTO
{
    public class FlightResponse
    {
        public Guid FlightID { get; set; }

        public string Number { get; set; }

        public DateTime DepartureDateUTC { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }

        public AircraftTypeResponse? AircraftType { get; set; }
    }
}
