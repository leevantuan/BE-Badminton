namespace Data_Transfer_Object.ProductDTO
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }

        public string ImgLink { get; set; }

        public double Quantity { get; set; }

        public double PriceSale { get; set; }

        public string Unit { get; set; }

        public bool IsStatus { get; set; }

        public Guid CategoryId { get; set; }
    }
}
