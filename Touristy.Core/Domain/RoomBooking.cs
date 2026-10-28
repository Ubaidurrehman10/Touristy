using Touristy.Core.Models;

namespace Touristy.Core.Domain
{
    public class RoomBooking : RoomBookingBase
    {
        public int RoomId { get; set; }
        public int Id { get; set; }
    }
}