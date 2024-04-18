namespace Data_Transfer_Object.ProductBillDTO
{
    public class GetProductBill
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid BillId { get; set; }

        public double Quantity { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsStatus { get; set; }
    }
}
