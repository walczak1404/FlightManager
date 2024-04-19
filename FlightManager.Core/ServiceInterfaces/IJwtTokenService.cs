using FlightManager.Core.Domain.Entities;
using FlightManager.Core.DTO;
using System.Security.Claims;

namespace FlightManager.Core.ServiceInterfaces
{
    /// <summary>
    /// Interface of service responsible for managing JWT Tokens
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token using the given user's information and the configuration settings.
        /// </summary>
        /// <param name="user">ApplicationUser object</param>
        /// <returns>AuthenticationResponse that includes token</returns>
        AuthenticationResponse CreateJwtToken(AppUser user);

        /// <summary>
        /// Retrieves claims from token
        /// </summary>
        /// <param name="token">Token to retrieve claims from</param>
        /// <returns>Retrieved claims</returns>
        ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
    }
}
