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

        //string length 6 because flight number consists of two-character airline designator and a 1 to 4 digit number
        [StringLength(6)]
        public string? Number { get; set; }

        //departure date time should be in UTC time to avoid different timezones problem
        public DateTime? DepartureDateUTC { get; set; }

        [StringLength(100)]
        public string? DepartureCity { get; set; }

        [StringLength(100)]
        public string? ArrivalCity { get; set; }

        public Guid? AircraftTypeID { get; set; }

        [ForeignKey(nameof(AircraftTypeID))]
        public virtual AircraftType? AircraftType { get; set; }
    }
}