namespace Data_Access_Layer.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDay { get; set; }

        public DateTime EndDay { get; set; }

        public double Precent { get; set; }

        public bool IsStatus { get; set; }

        //navigatiob
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
