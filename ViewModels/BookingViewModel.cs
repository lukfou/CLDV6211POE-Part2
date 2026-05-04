namespace CLDV6211POE.ViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
    }
}