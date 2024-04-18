using FlightManager.Core.DTO;
using FlightManager.Core.Enums;
using FlightManager.Core.ServiceInterfaces;
using FlightManager.Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        /// <summary>
        /// Gets nth 10 stored, filtered and sorted flights based on parameters
        /// </summary>
        /// <param name="pageNumber">Describes which nth 10 flights will be retrieved</param>
        /// <param name="sortType">Parameter flights will be sorted based on</param>
        /// <param name="sortOrder">Order of sorting (ASC or DESC)</param>
        /// <param name="departureCity">Filter of departure city</param>
        /// <param name="arrivalCity">Filter of arrival city</param>
        /// <returns>Fetched flights</returns>
        [HttpGet]
        public async Task<ActionResult<PagedList<FlightResponse>>> GetFlights(int? pageNumber = 1, SortType? sortType = SortType.DepartureDateUTC, SortOrder? sortOrder = SortOrder.ASC, string? departureCity = "", string? arrivalCity = "")
        {
            PagedList<FlightResponse> retrievedFlights;

            try
            {
                retrievedFlights = await _flightsService.GetFlightsAsync(pageNumber, sortType, sortOrder, departureCity, arrivalCity);
            } catch (Exception e)
            {
                return Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
            }

            return Ok(retrievedFlights);
        }

        /// <summary>
        /// Posts new flight
        /// </summary>
        /// <param name="flightPostRequest">Flight to add</param>
        /// <returns>Added flight</returns>
        [HttpPost]
        public async Task<ActionResult<FlightResponse>> PostFlight(FlightPostRequest flightPostRequest)
        {
            FlightResponse addedFlight;

            try
            {
                addedFlight = await _flightsService.PostFlightAsync(flightPostRequest);
            } catch (Exception e)
            {
                return Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
            }

            return Ok(addedFlight);
        }
    }
}
