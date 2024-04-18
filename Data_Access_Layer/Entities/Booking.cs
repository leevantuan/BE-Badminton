using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }

        public DateTime BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string UserId { get; set; }

        public bool IsStatus { get; set; }

        [ForeignKey("Court")]
        public Guid CourtId { get; set; }

        //navigate
        public virtual Court Court { get; set; }

        public virtual List<Bill> Bill { get; set; }

    }
}
