using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class ProductOrder
    {
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        public double Quantity { get; set; }

        public DateTime CreateAt { get; set; }

        //navigation
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
