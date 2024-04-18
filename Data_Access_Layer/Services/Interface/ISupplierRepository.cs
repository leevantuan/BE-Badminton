using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> GetAllAsync();

        public Task<Supplier?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Supplier supplier);

        public Task<bool> UpdateAsync(Supplier supplier);

        public Task<bool> DeleteAsync(Supplier supplier);
    }
}
