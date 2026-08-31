using Touristy.Core.DataServices;
using Touristy.Core.Domain;
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
            
            if (availableRools.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            }

            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
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