using FlightManager.Core.DTO;
using FlightManager.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    [Route("aircraft-types")]
    [ApiController]
    public class AircraftTypesController : ControllerBase
    {
        private readonly IAircraftTypesService _aircraftTypesService;

        public AircraftTypesController(IAircraftTypesService aircraftTypesService)
        {
            _aircraftTypesService = aircraftTypesService;
        }

        /// <summary>
        /// Gets all stored aircraft types
        /// </summary>
        /// <returns>List of stored types</returns>
        [HttpGet]
        [Authorize]
        public async Task<List<AircraftTypeResponse>> GetAircraftTypes()
        {
            List<AircraftTypeResponse> aircraftTypeResponses = await _aircraftTypesService.GetAllAircraftTypesAsync();

            return aircraftTypeResponses;
        }
    }
}
