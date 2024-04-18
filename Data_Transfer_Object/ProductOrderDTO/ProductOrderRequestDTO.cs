namespace Data_Transfer_Object.ProductOrderDTO
{
    public class ProductOrderRequestDTO
    {
        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public double Quantity { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
