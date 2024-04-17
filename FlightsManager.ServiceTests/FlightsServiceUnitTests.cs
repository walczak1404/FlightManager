using System.ComponentModel.DataAnnotations;

namespace FlightsManager.ServiceTests
{
    public class FlightsServiceUnitTests
    {
        private readonly IFlightsService _flightsService;
        private readonly Mock<IFlightsRepository> _flightsRepositoryMock;
        private readonly IFixture _fixture;

        public FlightsServiceUnitTests()
        {
            _fixture = new Fixture();
            _flightsRepositoryMock = new Mock<IFlightsRepository>();
            _flightsService = new FlightsService(_flightsRepositoryMock.Object);
        }

        #region GetFlightsAsync

        public async Task GetFligtsAsync_ShouldReturnArgumentException_WhenPageNumberIsLowerThanOne()
        {
            // Assert

        }

        #endregion

        #region PostFlightAsync

        [Fact]
        public async Task PostFlightAsync_ShouldThrowArgumentNullException_WhenGivenObjectIsNull()
        {
            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.PostFlightAsync(null);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task PostFlightAsync_ShouldThrowValidationException_WhenPropertiesAreNull()
        {
            // Arrange
            FlightPostRequest flightPostRequestWithNullProperties = new FlightPostRequest();

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.PostFlightAsync(flightPostRequestWithNullProperties);
            };

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task PostFlightAsync_ShouldThrowValidationException_WhenPropertyIsInvalid()
        {
            // Arrange
            FlightPostRequest sampleFlightPostRequestWithInvalidDepartureDate = _fixture.Build<FlightPostRequest>()
                .With(f => f.Number, "LO123")
                .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(-1))
                .With(f => f.DepartureCity, "Warszawa")
                .With(f => f.ArrivalCity, "Nowy Jork")
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.PostFlightAsync(sampleFlightPostRequestWithInvalidDepartureDate);
            };

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task PostFlightAsync_ShouldReturnAddedFlight_WhenProperObjectIsGiven()
        {
            // Arrange
            FlightPostRequest sampleFlightPostRequest = _fixture.Build<FlightPostRequest>()
                .With(f => f.Number, "LO123")
                .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(1))
                .With(f => f.DepartureCity, "Warszawa")
                .With(f => f.ArrivalCity, "Nowy Jork")
                .Create();

            Flight sampleFlight = sampleFlightPostRequest.ToFlight();
            sampleFlight.FlightID = Guid.NewGuid(); // normally database generates ID

            FlightResponse sampleFlightResponse = sampleFlight.ToFlightResponse();

            _flightsRepositoryMock.Setup(repo => repo.PostFlightAsync(It.IsAny<Flight>())).ReturnsAsync(sampleFlight);

            // Act
            FlightResponse retrievedFlightResponse = await _flightsService.PostFlightAsync(sampleFlightPostRequest);

            // Assert
            retrievedFlightResponse.Should().BeEquivalentTo(sampleFlightResponse);
        }

        #endregion

        #region PutFlightAsync

        [Fact]
        public async Task PutFlightAsync_ShouldThrowArgumentNullException_WhenGivenObjectIsNull()
        {
            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.PutFlightAsync(null);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task PutFlightAsync_ShouldThrowValidationException_WhenPropertiesAreNull()
        {
            // Arrange
            FlightPutRequest flightPutRequestWithNullProperties = new FlightPutRequest();

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.PutFlightAsync(flightPutRequestWithNullProperties);
            };
            
            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task PutFlightAsync_ShouldThrowValidationException_WhenPropertyIsInvalid()
        {
            // Arrange
            FlightPutRequest sampleFlightPutRequestWithInvalidNumber = _fixture.Build<FlightPutRequest>()
                .With(f => f.Number, "InvalidFlightNumber")
                .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(1))
                .With(f => f.DepartureCity, "Warszawa")
                .With(f => f.ArrivalCity, "Nowy Jork")
                .Create();

            // Act
            Func <Task> action = async () =>
            {
                await _flightsService.PutFlightAsync(sampleFlightPutRequestWithInvalidNumber);
            };

            // Assert
            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task PutFlightAsync_ShouldThrowArgumentException_WhenFlightWithGivenIDDoesNotExist()
        {
            // Arrange
            Guid sampleID = Guid.NewGuid();
            FlightPutRequest sampleFlightPutRequest = _fixture.Build<FlightPutRequest>()
                .With(f => f.Number, "LO123")
                .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(1))
                .With(f => f.DepartureCity, "Warszawa")
                .With(f => f.ArrivalCity, "Nowy Jork")
                .Create();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(sampleID)).ReturnsAsync(null as Flight);

            // Act

            Func<Task> action = async () =>
            {
                await _flightsService.PutFlightAsync(sampleFlightPutRequest);
            };

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task PutFlightAsync_ShouldReturnUpdatedFlight_WhenProperObjectIsGiven()
        {
            // Arrange
            FlightPutRequest sampleFlightPutRequest = _fixture.Build<FlightPutRequest>()
                .With(f => f.Number, "LO123")
                .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(1))
                .With(f => f.DepartureCity, "Warszawa")
                .With(f => f.ArrivalCity, "Nowy Jork")
                .Create();

            Flight sampleFlight = sampleFlightPutRequest.ToFlight();
            FlightResponse sampleFlightResponse = sampleFlight.ToFlightResponse();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(sampleFlight.FlightID!.Value)).ReturnsAsync(sampleFlight);
            _flightsRepositoryMock.Setup(repo => repo.PutFlightAsync(sampleFlightPutRequest)).ReturnsAsync(sampleFlight);

            // Act
            FlightResponse retrievedFlightResponse = await _flightsService.PutFlightAsync(sampleFlightPutRequest);

            // Assert
            retrievedFlightResponse.Should().BeEquivalentTo(sampleFlightResponse);
        }

        #endregion

        #region DeleteFlightAsync

        [Fact]
        public async Task DeleteFlightAsync_ShouldThrowArgumentException_WhenIDOfNonexistentFlightIsGiven()
        {
            // Arrange
            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(It.IsAny<Guid>())).ReturnsAsync(null as Flight);

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.DeleteFlightAsync(Guid.NewGuid());
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task DeleteFlightAsync_ShouldNotThrowException_WhenExistingFlightIDIsGiven()
        {
            // Arrange
            Flight sampleFlight = _fixture.Create<Flight>();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(It.IsAny<Guid>())).ReturnsAsync(sampleFlight);

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.DeleteFlightAsync(sampleFlight.FlightID!.Value);
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        #endregion
    }
}