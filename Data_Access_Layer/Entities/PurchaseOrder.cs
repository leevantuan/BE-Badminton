using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class PurchaseOrder
    {
        public Guid Id { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public DateTime CrateAt { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }

        public string UserId { get; set; }

        //Navigation
        public virtual Supplier Supplier { get; set; }

        public virtual Product Product { get; set; }
    }
}
