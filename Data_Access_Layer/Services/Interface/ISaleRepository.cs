using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface ISaleRepository
    {
        public Task<List<Sale>> GetAllAsync();

        public Task<Sale?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Sale sale);

        public Task<bool> UpdateAsync(Sale sale);

        public Task<bool> DeleteAsync(Sale sale);
    }
}
