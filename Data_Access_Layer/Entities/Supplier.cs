namespace Data_Access_Layer.Entities
{
    public class Supplier
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        //Navigation
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set;}
    }
}
