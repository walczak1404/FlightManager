using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FlightManager.Core.Enums;
using FlightManager.Core.Utilities;


namespace FlightsManager.ServiceTests
{
    public class FlightsServiceUnitTests
    {
        private readonly IFlightsService _flightsService;
        private readonly Mock<IFlightsRepository> _flightsRepositoryMock;
        private readonly Mock<IAircraftTypesRepository> _aircraftTypesRepositoryMock;
        private readonly IFixture _fixture;

        public FlightsServiceUnitTests()
        {
            _fixture = new Fixture();
            _flightsRepositoryMock = new Mock<IFlightsRepository>();
            _aircraftTypesRepositoryMock = new Mock<IAircraftTypesRepository>();
            _flightsService = new FlightsService(_flightsRepositoryMock.Object, _aircraftTypesRepositoryMock.Object);
        }

        #region GetFlightsAsync

        [Fact]
        public async Task GetFlightsAsync_ShouldThrowArgumentNullException_WhenOneOfParametersIsNull()
        {
            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.GetFlightsAsync(null, null, null, null);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetFlightsAsync_ShouldThrowArgumentException_WhenPageNumberIsLowerThanOne()
        {
            // Arrange
            int invalidPageNumberZero = 0;
            int invalidPageNumberNegative = -3;

            // Act
            Func<Task> actionZero = async () =>
            {
                await _flightsService.GetFlightsAsync(invalidPageNumberZero);
            };

            Func<Task> actionNegative = async () =>
            {
                await _flightsService.GetFlightsAsync(invalidPageNumberNegative);
            };

            // Assert
            await actionZero.Should().ThrowAsync<ArgumentException>();
            await actionNegative.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task GetFlightsAsync_ShouldReturnFlights_WhenCorrectParametersAreGiven()
        {
            // Arrange
            int pageNumber = 1;

            List<Flight> sampleFlights = new List<Flight>();

            for (int i = 0; i < 17; i++)
            {
                sampleFlights.Add(
                    _fixture.Build<Flight>()
                    .With(f => f.Number, $"LO12{i}")
                    .With(f => f.DepartureDateUTC, DateTime.UtcNow.AddDays(i+1))
                    .With(f => f.DepartureCity, $"Warszawa{i}")
                    .With(f => f.ArrivalCity, $"Nowy Jork{16-i}")
                    .Create()
                );
            }

            PagedList<Flight> sampleFlightsPagedList = new PagedList<Flight>(sampleFlights, 1, 2);
            PagedList<FlightResponse> sampleResponsesPagedList = new PagedList<FlightResponse>(sampleFlights.Select(f => f.ToFlightResponse()).ToList(), pageNumber, sampleFlightsPagedList.TotalPagesCount);

            
            _flightsRepositoryMock.Setup(repo => repo.GetFlightsAsync(pageNumber, It.IsAny<Expression<Func<Flight, bool>>>(), It.IsAny<Expression<Func<Flight, object>>>(), SortOrder.ASC)).ReturnsAsync(sampleFlightsPagedList);

            // Act
            PagedList<FlightResponse> retrievedResponse = await _flightsService.GetFlightsAsync(pageNumber, SortType.DepartureDateUTC, SortOrder.ASC);


            // Assert
            retrievedResponse.Should().BeEquivalentTo(sampleResponsesPagedList);
        }

        #endregion

        #region GetFlightByIDAsync

        [Fact]
        public async Task GetFlightByIDAsync_ShouldThrowArgumentNullException_WhenFlightIDIsNull()
        {
            // Arrange
            Guid? flightID = null;

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.GetFlightByIDAsync(flightID);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetFlightByIDAsync_ShouldThrowArgumentException_WhenFlightDoesNotExist()
        {
            // Arrange
            Guid flightID = Guid.NewGuid();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(flightID)).ReturnsAsync(null as Flight);

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.GetFlightByIDAsync(flightID);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task GetFlightByIDAsync_ShouldReturnFlight_WhenIDIsCorrect()
        {
            // Arrange
            Flight sampleFlight = _fixture.Create<Flight>();
            FlightResponse sampleFlightResponse = sampleFlight.ToFlightResponse();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(sampleFlight.FlightID)).ReturnsAsync(sampleFlight);

            // Act

            FlightResponse flightFromService = await _flightsService.GetFlightByIDAsync(sampleFlight.FlightID);

            // Assert
            flightFromService.Should().BeEquivalentTo(sampleFlightResponse);
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

            AircraftType sampleAircraftType = _fixture.Create<AircraftType>();

            _aircraftTypesRepositoryMock.Setup(repo => repo.GetAircraftTypeByIDAsync(It.IsAny<Guid>())).ReturnsAsync(sampleAircraftType);
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

            // Assert
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

            AircraftType sampleAircraftType = _fixture.Create<AircraftType>();
            _aircraftTypesRepositoryMock.Setup(repo => repo.GetAircraftTypeByIDAsync(It.IsAny<Guid>())).ReturnsAsync(sampleAircraftType);

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(sampleFlight.FlightID)).ReturnsAsync(sampleFlight);
            _flightsRepositoryMock.Setup(repo => repo.PutFlightAsync(sampleFlightPutRequest)).ReturnsAsync(sampleFlight);

            // Act
            FlightResponse retrievedFlightResponse = await _flightsService.PutFlightAsync(sampleFlightPutRequest);

            // Assert
            retrievedFlightResponse.Should().BeEquivalentTo(sampleFlightResponse);
        }

        #endregion

        #region DeleteFlightAsync

        [Fact]
        public async Task DeleteFlightAsync_ShouldThrowArgumentNullException_WhenNullIDIsGiven()
        {
            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.DeleteFlightAsync(null);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

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
        public async Task DeleteFlightAsync_ShouldNotThrowException_WhenFlightIsDeleted()
        {
            // Arrange
            Flight sampleFlight = _fixture.Create<Flight>();

            _flightsRepositoryMock.Setup(repo => repo.GetFlightByIDAsync(It.IsAny<Guid>())).ReturnsAsync(sampleFlight);

            // Act
            Func<Task> action = async () =>
            {
                await _flightsService.DeleteFlightAsync(sampleFlight.FlightID); //HERE
            };

            // Assert
            await action.Should().NotThrowAsync();
        }

        #endregion
    }
}