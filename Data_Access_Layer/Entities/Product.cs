using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImgLink { get; set; }

        public double Quantity { get; set; }

        public double PriceSale { get; set; }

        public int? Size { get; set; }

        public string Unit { get; set; }

        public string? Brand { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public bool IsStatus { get; set; }

        //navigation
        public virtual Category Category { get; set; }

        public virtual ICollection<Vote> Vote { get; set;}

        public virtual ICollection<Comment> Comment { get; set;}

        public virtual ICollection<ProductOrder> ProductOrder { get; set; }

        public virtual ICollection<ProductBill> ProductBill { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }

    }
}
