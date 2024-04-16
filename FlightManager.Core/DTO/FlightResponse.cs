using FlightManager.Core.Domain.Entities;

namespace FlightManager.Core.DTO
{
    /// <summary>
    /// Flight DTO for sending flight data to client
    /// </summary>
    public class FlightResponse
    {
        public Guid FlightID { get; set; }

        public string Number { get; set; }

        public DateTime DepartureDateUTC { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }

        public AircraftTypeResponse? AircraftType { get; set; }
    }

    public static class FlightExtensions
    {
        public static FlightResponse ToFlightResponse(this Flight flight)
        {
            return new()
            {
                FlightID = flight.FlightID,
                Number = flight.Number,
                DepartureDateUTC = flight.DepartureDateUTC.Value,
                DepartureCity = flight.DepartureCity,
                ArrivalCity = flight.ArrivalCity,
                AircraftType = flight.AircraftType != null ? flight.AircraftType.ToAircraftTypeResponse() : null
            };
        }
    }
}
