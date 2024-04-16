using AutoFixture;
using FlightManager.Core.Domain.RepositoryInterfaces;
using FlightManager.Core.ServiceInterfaces;
using Moq;

namespace FlightsManager.ServiceTests
{
    public class FlightsServiceTests
    {
        private readonly IFlightsService _flightsService;

        private readonly Mock<IFlightsRepository> _flightsRepositoryMock;
        private readonly IFlightsRepository _flightsRepository;

        private readonly IFixture _fixture;

        public FlightsServiceTests()
        {
            _fixture = new Fixture();
            _flightsRepositoryMock = new Mock<IFlightsRepository>();
            _flightsRepository = _flightsRepositoryMock.Object;


        }
    }
}