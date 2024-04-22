using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.DTO
{
    public class TokenModel
    {
        [Required(ErrorMessage = "Nie podano wygaśniętego tokena")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "Nie podano tokena odświeżającego")]
        public string? RefreshToken { get; set; }
    }
}
