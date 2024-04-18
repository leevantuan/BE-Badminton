using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IBillRepository
    {
        public Task<List<Bill>> GetAllAsync();

        public Task<Bill?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Bill bill);

        public Task<bool> DeleteAsync(Bill bill);
    }
}
