using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;

namespace Business_Logic_Layer.VoteBLL
{
    public interface IVoteRepositoryBLL
    {
        public Task<List<GetVote>> GetAllAsync(GetAllRequestModel request);

        public Task<GetVote?> GetByIdAsync(Guid id);

        public Task<List<GetVoteDetail>> GetByProductIdAsync(Guid productId);

        public Task<bool> CreateAsync(VoteRequestDTO comment);

        public Task<bool> UpdateAsync(Guid id, VoteRequestDTO comment);

        public Task<bool> DeleteAsync(Guid id);
    }
}
