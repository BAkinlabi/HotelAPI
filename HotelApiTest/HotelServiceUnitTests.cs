using Moq; 
using HotelAPI.Models; 
using HotelAPI.Repositories;
using HotelAPI.Services;

namespace HotelApiTest
{
    public class HotelServiceTests
    {
        private readonly IHotelService _hotelService;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly IBookingService _bookingService;
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly IRoomService _roomService;
        private readonly Mock<IRoomRepository> _roomRepositoryMock;

        public HotelServiceTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _hotelService = new HotelService(_hotelRepositoryMock.Object);


            _roomRepositoryMock = new Mock<IRoomRepository>();
            _roomService = new RoomService(_roomRepositoryMock.Object);

            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _bookingService = new BookingService(_roomRepositoryMock.Object, _bookingRepositoryMock.Object);
        }

        [Fact]
        public async Task GetHotelByName_ShouldReturnHotel_WhenHotelExists()
        {
            // Arrange
            var hotelName = "Test Hotel";
            var hotel = new Hotel { Name = hotelName };
            _hotelRepositoryMock.Setup(repo => repo.GetHotelByNameAsync(hotelName)).ReturnsAsync(hotel);

            // Act
            var result = await _hotelService.GetHotelByNameAsync(hotelName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotelName, result.Name);
        }
        
        [Fact]
        public async Task GetBookingByReference_ShouldReturnBooking_WhenBookingExists()
        {
            // Arrange
            var bookingReference = "92e6a772-a40c-49ec-b8cb-d56ef926ecdd";
            var booking = new Booking { BookingNumber = bookingReference };
            _bookingRepositoryMock.Setup(repo => repo.GetBookingByReferenceNumberAsync(bookingReference)).ReturnsAsync(booking);

            // Act
            var result = await _bookingService.GetBookingByReferenceNumberAsync(bookingReference);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bookingReference, result.BookingNumber);
        }

        
    }

}