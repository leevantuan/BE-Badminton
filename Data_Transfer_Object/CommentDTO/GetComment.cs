namespace Data_Transfer_Object.CommentDTO
{
    public class GetComment
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string Content { get; set; }

        public Guid ProductId { get; set; }
    }
}
