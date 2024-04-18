using Data_Access_Layer.Entities;
using Data_Transfer_Object.ProductDTO;

namespace Data_Access_Layer.Services.Interface
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();

        public Task<Product?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Product product);

        public Task<bool> UpdateAsync(Product product);

        public Task<bool> InceaseQuantityAsync(Product product, ChangeQuantity quantity);

        public Task<bool> ReduceQuantityAsync(Product product, ChangeQuantity quantity);

        public Task<bool> DeleteAsync(Product product);
    }
}
