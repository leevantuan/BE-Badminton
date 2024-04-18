using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public double VoteNumber { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        //navigation
        public virtual Product Product { get; set; }
    }
}
