namespace Data_Transfer_Object.CommentDTO
{
    public class GetCommentDetail
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }
    }
}
