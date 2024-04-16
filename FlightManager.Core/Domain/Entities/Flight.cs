using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManager.Core.Domain.Entities
{
    /// <summary>
    /// Flight model used to create database table
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

        [ForeignKey(nameof(AircraftTypeID))]
        public virtual AircraftType? AircraftType { get; set; }
    }
}