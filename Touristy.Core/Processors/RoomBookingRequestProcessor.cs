using Touristy.Core.DataServices;
using Touristy.Core.Domain;
using Touristy.Core.Enums;
using Touristy.Core.Models;

namespace Touristy.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;
        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException("bookingRequest");    
            };

            var availableRools = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (availableRools.Any())
            {
                var room = availableRools.First();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
                roomBooking.RoomId = room.Id;
                _roomBookingService.Save(roomBooking);

                result.Flag = BookingResultFlag.Success;
            }
            else 
            {
                result.Flag = BookingResultFlag.Failure;
            }

            return result;
        }

        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking
            : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                Name = bookingRequest.Name,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date
            };
        }
    }
}