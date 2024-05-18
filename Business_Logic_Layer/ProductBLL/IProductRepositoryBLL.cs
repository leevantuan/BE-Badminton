using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;

namespace Business_Logic_Layer.ProductBLL
{
    public interface IProductRepositoryBLL
    {
        public Task<List<GetProduct>> GetAllAsync(int pageNumber, int pageSize, string filterQuery);

        public Task<GetProduct?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(ProductRequestDTO product);

        public Task<bool> UpdateAsync(Guid id, ProductRequestDTO product);

        public Task<bool> InceaseQuantityAsync(Guid id, ChangeQuantity quantity);

        public Task<bool> ReduceQuantityAsync(Guid id, ChangeQuantity quantity);

        public Task<bool> DeleteAsync(Guid id);

        public Task<List<GetProduct>> GetByCategoryId(Guid categoryId);

        public Task<int> TotalPage(double pageSize, string filterQuery);

    }
}
