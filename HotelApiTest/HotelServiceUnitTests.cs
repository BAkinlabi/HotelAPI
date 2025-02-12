using Moq; 
using HotelAPI.Models; 
using HotelAPI.Repositories;
using HotelAPI.Services;
using HotelAPI.ModelDTOs;

namespace HotelApiTest
{
    public class HotelServiceTests
    {
        private readonly IHotelService _hotelService;
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly IBookingService _bookingService;
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;


        public HotelServiceTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _hotelService = new HotelService(_hotelRepositoryMock.Object);

            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _bookingService = new BookingService(_bookingRepositoryMock.Object);
        }

        [Fact]
        public async Task GetHotelByName_ShouldReturnHotelDTO_WhenHotelExists()
        {
            // Arrange
            var hotelName = "Test Hotel";
            var hotelDTO = new HotelDTO { Name = hotelName }; 
            _hotelRepositoryMock.Setup(repo => repo.GetHotelByNameAsync(hotelName)).ReturnsAsync(hotelDTO);

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
            var bookingReference = "BookingRer001";
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