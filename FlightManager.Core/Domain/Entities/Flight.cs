using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.Domain.Entities
{
    /// <summary>
    /// Flight model for managing database operations
    /// </summary>
    public class Flight
    {
        [Key]
        public Guid FlightID { get; set; }

        public int? Number { get; set; }

        public DateTime? DepartureDate { get; set; }

        [StringLength(100)]
        public string? DepartureAirport { get; set; }

        [StringLength(100)]
        public string? ArrivalAirport { get; set; }

        public Guid? AircraftTypeID { get; set; }
    }
}