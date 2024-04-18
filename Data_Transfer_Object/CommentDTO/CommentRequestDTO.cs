namespace Data_Transfer_Object.VoteDTO
{
    public class CommentRequestDTO
    {
        public string UserId { get; set; }

        public string Content { get; set; }

        public Guid ProductId { get; set; }
    }
}
