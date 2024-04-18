using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        //navigation
        public virtual Product Product { get; set; }
    }
}
