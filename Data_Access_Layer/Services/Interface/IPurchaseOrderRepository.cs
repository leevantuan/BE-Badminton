using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IPurchaseOrderRepository
    {
        public Task<List<PurchaseOrder>> GetAllAsync();

        public Task<PurchaseOrder?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(PurchaseOrder purchase);

        public Task<bool> UpdateAsync(PurchaseOrder purchase);

        public Task<bool> DeleteAsync(PurchaseOrder purchase);
    }
}
