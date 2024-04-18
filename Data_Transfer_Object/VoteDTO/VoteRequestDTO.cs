using System.ComponentModel.DataAnnotations;

namespace Data_Transfer_Object.VoteDTO
{
    public class VoteRequestDTO
    {
        public string UserId { get; set; }

        [Range(1, 5, ErrorMessage = "Error! vote number range 1 - 5")]
        public double VoteNumber { get; set; }

        public Guid ProductId { get; set; }
    }
}
