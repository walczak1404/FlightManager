using System.ComponentModel.DataAnnotations;

namespace FlightManager.Core.Services.Helpers
{
    internal class Validation
    {
        internal static void Validate(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            ValidationContext context = new ValidationContext(obj);

            List<ValidationResult> errors = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, context, errors, true);

            if (!isValid) throw new ValidationException(errors.First().ErrorMessage);
        }
    }
}
