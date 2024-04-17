namespace FlightManager.Core.DTO
{
    /// <summary>
    /// Flight filtering model used to return only flights which match user request
    /// </summary>
    public class FlightFilter
    {
        public string DepartureCity { get; set; } = "";

        public string ArrivalCity { get; set; } = "";
    }
}
