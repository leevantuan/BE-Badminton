namespace Data_Transfer_Object.VoteDTO
{
    public class GetVote
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public double VoteNumber { get; set; }

        public Guid ProductId { get; set; }
    }
}
