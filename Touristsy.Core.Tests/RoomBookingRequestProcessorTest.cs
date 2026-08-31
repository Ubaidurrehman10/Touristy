using Moq;
using Shouldly;
using Touristy.Core.DataServices;
using Touristy.Core.Domain;
using Touristy.Core.Models;
using Touristy.Core.Processors;

namespace Touristy.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor _processor;
        private RoomBookingRequest _request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _request = new RoomBookingRequest
            {
                Name = "Ubaid ur rehman",
                Email = "Ubaidurrehman@outlook.com",
                Date = new DateTime(2026, 09, 05)
            };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void ShouldReturnRoomBookingResponseWithRequestValues()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.Name, result.Name);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(_request.Name);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);
        }

        [Fact]
        public void ShouldThrowExceptionForNullRequest()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null!));
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void ShouldSaveRoomBookingRequest()
        {
            RoomBooking savedBooking = null!;
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.Name.ShouldBe(_request.Name);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);
        }
    }
}
