namespace Data_Access_Layer.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public DateTime CreateAt { get; set; }

        public double Total { get; set; }

        public bool IsStatus { get; set; }

        //navigation
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
