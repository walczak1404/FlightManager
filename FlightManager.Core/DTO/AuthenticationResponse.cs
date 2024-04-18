namespace FlightManager.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? Email { get; set; }

        public string? Token { get; set; }

        public DateTime ExpirationTimeUTC { get; set; }
    }
}
