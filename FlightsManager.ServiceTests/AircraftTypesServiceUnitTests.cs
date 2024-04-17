using FluentAssertions;

namespace FlightsManager.ServiceTests
{
    public class AircraftTypesServiceUnitTests
    {
        private readonly IAircraftTypesService _aircraftTypesService;
        private readonly Mock<IAircraftTypesRepository> _aircraftTypesRepositoryMock;
        private readonly IFixture _fixture;

        public AircraftTypesServiceUnitTests()
        {
            _fixture = new Fixture();
            _aircraftTypesRepositoryMock = new Mock<IAircraftTypesRepository>();
            _aircraftTypesService = new AircraftTypesService(_aircraftTypesRepositoryMock.Object);
        }

        #region GetAllAircraftTypes

        [Fact]
        public async Task GetAllAircraftTypes_ShoultReturnEmptyList_WithNoTypesStored()
        {
            // Arrange
            _aircraftTypesRepositoryMock.Setup(repo => repo.GetAllAircraftTypesAsync()).ReturnsAsync(new List<AircraftType>());

            // Act
            List<AircraftTypeResponse> retrievedAircraftTypeResponses = await _aircraftTypesService.GetAllAircraftTypesAsync();

            // Assert
            retrievedAircraftTypeResponses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAircraftTypes_ShouldReturnAllTypes_WithFewTypesStored()
        {
            // Arrange
            List<AircraftType> sampleTypes = new List<AircraftType>()
            {
                _fixture.Create<AircraftType>(),
                _fixture.Create<AircraftType>(),
                _fixture.Create<AircraftType>()
            };

            List<AircraftTypeResponse> sampleTypeResponses = sampleTypes.Select(type => type.ToAircraftTypeResponse()).ToList();

            _aircraftTypesRepositoryMock.Setup(repo => repo.GetAllAircraftTypesAsync()).ReturnsAsync(sampleTypes);

            // Act
            List<AircraftTypeResponse> retrievedTypeResponses = await _aircraftTypesService.GetAllAircraftTypesAsync();

            // Assert
            retrievedTypeResponses.Should().BeEquivalentTo(sampleTypeResponses);
        }

        #endregion
    }
}
