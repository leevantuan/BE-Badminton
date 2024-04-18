namespace Data_Transfer_Object.PurchaseOrderDTO
{
    public class PurchaseOrderRequestDTO
    {
        public double Quantity { get; set; } = 0;

        public double Price { get; set; }

        public DateTime CrateAt { get; set; }

        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }

        public string UserId { get; set; }
    }
}
