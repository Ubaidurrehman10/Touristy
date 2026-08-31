using Touristy.Core.Domain;

namespace Touristy.Core.DataServices
{
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}