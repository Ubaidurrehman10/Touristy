using Touristy.Core.Enums;
using Touristy.Domain.BaseModels;

namespace Touristy.Core.Models
{
    public class RoomBookingResult : RoomBookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? RoomBookingId { get; set; }
    }
}