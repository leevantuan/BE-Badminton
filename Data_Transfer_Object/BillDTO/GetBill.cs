namespace Data_Transfer_Object.BillDTO
{
    public class GetBill
    {
        public Guid Id { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string CustomerId { get; set; }

        public string EmployeeId { get; set; }

        public Guid? BookingId { get; set; }

        public Guid? SaleId { get; set; }

        public Guid CourtId { get; set; }

        public string? Description { get; set; }

        public double Total { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsStatus { get; set; }
    }
}
