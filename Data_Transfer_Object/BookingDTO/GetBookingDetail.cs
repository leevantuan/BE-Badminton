namespace Data_Transfer_Object.BookingDTO
{
    public class GetBookingDetail
    {
        public Guid Id { get; set; }

        public DateTime BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string UserName { get; set; }

        public bool IsStatus { get; set; }

        public string CourtName { get; set; }
    }
}
