namespace FlightManager.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public DateTime ExpirationDateTimeUTC { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpirationDateTimeUTC { get; set; }
    }
}
