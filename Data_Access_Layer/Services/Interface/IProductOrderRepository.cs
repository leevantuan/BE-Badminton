using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface IProductOrderRepository
    {
        public Task<List<ProductOrder>> GetAllAsync();

        public Task<ProductOrder?> GetByIdAsync(Guid id);

        public Task<List<ProductOrder>> GetByProductIdAsync(Guid productId);

        public Task<bool> CreateAsync(ProductOrder productOrder);

        //public Task<bool> UpdateAsync(Guid id, ProductOrderUpdate productOrder);

        public Task<bool> DeleteAsync(ProductOrder productOrder);
    }
}
