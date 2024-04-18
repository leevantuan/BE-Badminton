namespace Data_Transfer_Object.BookingDTO
{
    public class BookingRequestDTO
    {
        public DateTime BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string UserId { get; set; }

        public bool IsStatus { get; set; }

        public Guid CourtId { get; set; }
    }
}
