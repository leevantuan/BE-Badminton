using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IProductBillRepository
    {
        public Task<List<ProductBill>> GetAllAsync();

        public Task<ProductBill?> GetByIdAsync(Guid id);

        public Task<List<ProductBill>> GetByProductIdAsync(Guid productId);

        public Task<bool> CreateAsync(ProductBill productBill);

        //public Task<bool> UpdateAsync(ProductBill productBill);

        public Task<bool> DeleteAsync(ProductBill productBill);
    }
}
