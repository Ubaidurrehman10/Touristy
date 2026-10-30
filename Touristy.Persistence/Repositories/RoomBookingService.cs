using Touristy.Core.DataServices;
using Touristy.Domain;

namespace Touristy.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly TouristyAppDbContext _Context;
        public RoomBookingService(TouristyAppDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            return _Context.Rooms.Where(r => !r.RoomBookings.Any(b => b.Date == date)).ToList();
        }

        public void Save(RoomBooking roomBooking) 
        {
            _Context.Add(roomBooking);
            _Context.SaveChanges();
        }
    }
}