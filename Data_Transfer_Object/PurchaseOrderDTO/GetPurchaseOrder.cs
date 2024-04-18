namespace Data_Transfer_Object.PurchaseOrderDTO
{
    public class GetPurchaseOrder
    {
        public Guid Id { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public Guid ProductId { get; set; }

        public Guid SupplierId { get; set; }

        public DateTime CrateAt { get; set; }

        public string UserId { get; set; }
    }
}
