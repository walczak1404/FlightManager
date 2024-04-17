namespace FlightsManager.ServiceTests
{
    public class FlightsServiceUnitTests
    {
        private readonly IFlightsService _flightsService;

        private readonly Mock<IFlightsRepository> _flightsRepositoryMock;
        private readonly IFlightsRepository _flightsRepository;

        private readonly IFixture _fixture;

        public FlightsServiceUnitTests()
        {
            _fixture = new Fixture();
            _flightsRepositoryMock = new Mock<IFlightsRepository>();
            _flightsRepository = _flightsRepositoryMock.Object;


        }
    }
}