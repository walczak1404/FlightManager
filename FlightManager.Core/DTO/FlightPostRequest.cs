using FlightManager.Core.CustomValidation;
using FlightManager.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.DTO
{
    /// <summary>
    /// Flight DTO for posting new flights
    /// </summary>
    public class FlightPostRequest
    {
        [Required(ErrorMessage = "Numer lotu jest wymagany")]
        [RegularExpression(@"^[A-Z]{2}\d{1,4}$", ErrorMessage = "Numer lotu musi składać się z 2 wielkich liter reprezentujących linię oraz od 1 do 4 cyfr")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "Data wylotu jest wymagana")]
        [FutureDateValidation]
        public DateTime DepartureDateUTC { get; set; }

        [Required(ErrorMessage = "Miejsce wylotu jest wymagane")]
        [StringLength(100, ErrorMessage = "Miejsce wylotu może składać się maksymalnie ze 100 znaków")]
        public string? DepartureCity { get; set; }

        [Required(ErrorMessage = "Miejsce przylotu jest wymagane")]
        [StringLength(100, ErrorMessage = "Miejsce przylotu może składać się maksymalnie ze 100 znaków")]
        public string? ArrivalCity { get; set; }

        [Required(ErrorMessage = "Typ samolotu jest wymagany")]
        public Guid AircraftTypeID { get; set; }


        public Flight ToFlight()
        {
            return new()
            {
                Number = Number,
                DepartureDateUTC = DepartureDateUTC,
                DepartureCity = DepartureCity,
                ArrivalCity = ArrivalCity,
                AircraftTypeID = AircraftTypeID
            };
        }
    }
}
