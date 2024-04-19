using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        [EmailAddress(ErrorMessage = "Adres email musi miec poprawny format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Hasło musi miec od 4 do 40 znaków")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą być takie same")]
        public string? ConfirmPassword { get; set; }
    }
}
