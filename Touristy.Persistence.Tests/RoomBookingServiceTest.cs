using Microsoft.EntityFrameworkCore;
using Touristy.Domain;
using Touristy.Persistence.Repositories;
using Xunit;

namespace Touristy.Persistence.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void ShouldReturnAvailableRooms()
        {
            //Arrange
            DateTime date = new DateTime(2026, 09, 05);

            //.UseInMemoryDatabase("AvailableRoomTest")
            var dbOptions = new DbContextOptionsBuilder<TouristyAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new TouristyAppDbContext(dbOptions);
            context.Add(new Room { Id = 1, Name = "Room 1" });
            context.Add(new Room { Id = 2, Name = "Room 2" });
            context.Add(new Room { Id = 3, Name = "Room 3" });

            context.Add(new RoomBooking { RoomId = 1, Date = date  });
            context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //Act
            var availableRooms = roomBookingService.GetAvailableRooms(date);

            //Assert
            Assert.Equal(2, availableRooms.Count());
            Assert.Contains(availableRooms, q => q.Id == 2);
            Assert.Contains(availableRooms, q => q.Id == 3);
            Assert.DoesNotContain(availableRooms, q => q.Id == 1);
        }

        [Fact]
        public void ShouldSaveRoomBooking()
        {
            //Arrange
            var dbOptions = new DbContextOptionsBuilder<TouristyAppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var roomBooking = new RoomBooking { Id = 1, Date = new DateTime(2026, 09, 05) };

            //Act
            using var context = new TouristyAppDbContext(dbOptions);
            var roomBookingService = new RoomBookingService(context);
            roomBookingService.Save(roomBooking);

            //Assert
            var bookings = context.RoomBookings.ToList();
            var booking = Assert.Single(bookings);

            Assert.Equal(roomBooking.Id, booking.Id);
            Assert.Equal(roomBooking.Date, booking.Date);
        }
    }
}