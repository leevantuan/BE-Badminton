namespace Data_Access_Layer.Entities
{
    public class Court
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price {  get; set; }

        public string? Description { get; set; }

        public bool IsStatus { get; set; }

        //navigate
        public virtual ICollection<Booking> Booking { get; set; }

        public virtual ICollection<Bill> Bill { get; set; }

    }
}
