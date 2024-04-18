using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface ICourtRepository
    {
        public Task<List<Court>> GetAllAsync();

        public Task<Court?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Court court);

        public Task<bool> UpdateAsync(Court court);

        public Task<bool> DeleteAsync(Court court);
    }
}
