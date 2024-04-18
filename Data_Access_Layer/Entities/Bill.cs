using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Bill
    {
        public Guid Id { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public string? CustomerId { get; set; }

        public string EmployeeId { get; set; }

        [ForeignKey("Booking")]
        public Guid? BookingId { get; set; }

        [ForeignKey("Sale")]
        public Guid? SaleId { get; set; }

        [ForeignKey("Court")]
        public Guid? CourtId { get; set; }

        public double Total { get; set; }

        public string? Description { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsStatus { get; set; }
        
        //navigation
        public virtual Sale Sale { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Court Court { get; set; }

        public virtual ICollection<ProductBill> ProductBill { get; set; }
    }
}
