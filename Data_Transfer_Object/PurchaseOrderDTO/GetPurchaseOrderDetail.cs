namespace Data_Transfer_Object.PurchaseOrderDTO
{
    public class GetPurchaseOrderDetail
    {
        public Guid Id { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public string ProductName { get; set; }

        public string SupplierName { get; set; }

        public DateTime CrateAt { get; set; }

        public string UserName { get; set; }
    }
}
