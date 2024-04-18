using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IVoteRepository
    {
        public Task<List<Vote>> GetAllAsync();

        public Task<Vote?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Vote vote);

        public Task<bool> UpdateAsync(Vote vote);

        public Task<bool> DeleteAsync(Vote vote);
    }
}
