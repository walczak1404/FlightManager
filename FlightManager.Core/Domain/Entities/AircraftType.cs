using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.Domain.Entities
{
    /// <summary>
    /// Aircraft type model for managing database operations
    /// </summary>
    public class AircraftType
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(60)]
        public string? Name { get; set; }
    }
}