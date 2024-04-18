using Data_Transfer_Object.GetAll;

namespace Data_Transfer_Object.BillDTO
{
    public class BillDetail
    {
        public Guid Id { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string CustomerName { get; set; }

        public string EmployeeName { get; set; }

        public Guid? BookingId { get; set; }

        public Guid? SaleId { get; set; }

        public Guid CourtId { get; set; }

        public string? Description { get; set; }

        public double? Total { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsStatus { get; set; }

        public List<ProductDetail> Products { get; set; }
    }
}
