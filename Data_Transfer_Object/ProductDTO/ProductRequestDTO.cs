using System.ComponentModel.DataAnnotations;

namespace Data_Transfer_Object.ProductDTO
{
    public class ProductRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImgLink { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double PriceSale { get; set; }

        public int? Size { get; set; }

        [Required]
        public string Unit { get; set; }

        public string? Brand { get; set; }

        [Required]
        public bool IsStatus { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
