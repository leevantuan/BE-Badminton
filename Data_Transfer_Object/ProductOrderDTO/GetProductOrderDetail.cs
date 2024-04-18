namespace Data_Transfer_Object.ProductOrderDTO
{
    public class GetProductOrderDetail
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string UserName { get; set; }

        public double Quantity { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
