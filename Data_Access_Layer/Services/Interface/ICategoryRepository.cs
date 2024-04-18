using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Services.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync();

        public Task<Category?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Category category);

        public Task<bool> UpdateAsync(Category category);

        public Task<bool> DeleteAsync(Category category);
    }
}
