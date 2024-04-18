namespace Data_Transfer_Object.ProductBillDTO
{
    public class GetProductBillDetail
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public Guid BillId { get; set; }

        public double Quantity { get; set; }

        public bool IsStatus { get; set; }

        public DateTime CreateAt { get; set; }

    }
}
