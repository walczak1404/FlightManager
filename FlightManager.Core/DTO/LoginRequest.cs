using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.DTO
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string? Password { get; set; }
    }
}
