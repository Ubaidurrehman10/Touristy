namespace Touristy.Domain.BaseModels
{
    public class RoomBookingBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
