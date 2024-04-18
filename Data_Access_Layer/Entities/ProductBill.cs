using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class ProductBill
    {
        public Guid Id { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [ForeignKey("Bill")]
        public Guid BillId { get; set; }

        public double Quantity { get; set; }

        public DateTime CreateAt { get; set; }

        //navigation
        public virtual Product Product { get;}

        public virtual Bill Bill { get;}
    }
}
