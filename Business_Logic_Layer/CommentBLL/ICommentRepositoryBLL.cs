using Data_Transfer_Object.CommentDTO;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;

namespace Business_Logic_Layer.CommentBLL
{
    public interface ICommentRepositoryBLL
    {
        public Task<List<GetComment>> GetAllAsync(GetAllRequestModel request);

        public Task<GetComment?> GetByIdAsync(Guid id);

        public Task<List<GetCommentDetail>> GetByProductIdAsync(Guid productId);

        public Task<bool> CreateAsync(CommentRequestDTO comment);

        public Task<bool> UpdateAsync(Guid id, CommentRequestDTO comment);

        public Task<bool> DeleteAsync(Guid id);
    }
}
