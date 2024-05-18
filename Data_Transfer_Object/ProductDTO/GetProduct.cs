namespace Data_Transfer_Object.ProductDTO
{
    public class GetProduct
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImgLink { get; set; }

        public double Quantity { get; set; }

        public double SoldQuantity { get; set; }

        public double PriceSale { get; set; }

        public int? Size { get; set; }

        public string? Brand { get; set; }

        public string Unit { get; set; }

        public bool IsStatus { get; set; }

        public Guid CategoryId { get; set; }
    }
}
