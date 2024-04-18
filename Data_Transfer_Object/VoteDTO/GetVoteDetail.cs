namespace Data_Transfer_Object.VoteDTO
{
    public class GetVoteDetail
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public double VoteNumber { get; set; }
    }
}
