using Microsoft.AspNetCore.Identity;

namespace FlightManager.Core.Domain.Entities
{
    /// <summary>
    /// User model for authentication management
    /// </summary>
    public class AppUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}
