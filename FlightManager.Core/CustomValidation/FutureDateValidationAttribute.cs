using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.CustomValidation
{
    /// <summary>
    /// Custom validation for checking if date is not from past
    /// </summary>
    public class FutureDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? dateTimeValue = (DateTime?)value;

            if (dateTimeValue == null) return new ValidationResult("Nie podano poprawnej daty");

            if(dateTimeValue > DateTime.UtcNow)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Podana data jest przeszła");
            }
        }
    }
}
