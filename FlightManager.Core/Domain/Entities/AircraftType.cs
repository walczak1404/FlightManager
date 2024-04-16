using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.Domain.Entities
{
    /// <summary>
    /// Aircraft type model used to create database table
    /// </summary>
    public class AircraftType
    {
        [Key]
        public Guid AircraftTypeID { get; set; }

        [StringLength(60)]
        public string? Name { get; set; }
    }
}