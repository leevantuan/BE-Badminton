using Data_Transfer_Object.CategoryDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.CategoryBLL
{
    public interface ICategoryRepositoryBLL
    {
        public Task<List<GetCategory>> GetAllAsync();

        public Task<List<GetCategory>> GetPaginationAsync(int pageNumber, int pageSize);

        public Task<GetCategory?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(CategoryRequestDTO category);

        public Task<bool> UpdateAsync(Guid id, CategoryUpdate category);

        public Task<bool> DeleteAsync(Guid id);

        public Task<int> TotalPage(double pageSize);

    }
}
