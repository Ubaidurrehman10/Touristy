using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Touristy.Api.Controllers;
using Touristy.Core.Enums;
using Touristy.Core.Models;
using Touristy.Core.Processors;

namespace Touristy.Api.Tests
{
    public class RoomBookingControllerTests
    {
        private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
        private RoomBookingController _controller;
        private RoomBookingRequest _request;
        private RoomBookingResult _result;

        public RoomBookingControllerTests()
        {
            _roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
            _controller = new RoomBookingController(_roomBookingProcessor.Object);
            _request = new RoomBookingRequest();
            _result = new RoomBookingResult();
            
            _roomBookingProcessor.Setup(q => q.BookRoom(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public async Task ShouldCallBookingMethodWhenValid(int expextedMethodCalls, bool isModelValid,
            Type expectedActionResultType, BookingResultFlag bookingResultFlag)
        {
            //Arrange
            if(!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            _result.Flag = bookingResultFlag;

            //Act
            var result = await _controller.BookRoom(_request);
            
            //Assert
            result.ShouldBeOfType(expectedActionResultType);
            _roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expextedMethodCalls));
        }

        [Fact]
        public void ShouldReturnForcastResults()
        {
            var controller = new WeatherForecastController();
            var result = controller.Get();

            result.Count().ShouldBeGreaterThan(1);
            result.ShouldNotBeNull();
        }
    }
}
