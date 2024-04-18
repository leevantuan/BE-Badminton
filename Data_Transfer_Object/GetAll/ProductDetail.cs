namespace Data_Transfer_Object.GetAll
{
    public class ProductDetail
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImgLink { get; set; }

        public double Price { get; set; }

        public double Quantity { get; set; }

        public double Total { get; set; }
    }
}
