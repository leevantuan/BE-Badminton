using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly MyDBContext context;

        public SQLCategoryRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Category category)
        {
            try
            {
                await context.Category.AddAsync(category);
                var result = await context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //delete
        public async Task<bool> DeleteAsync(Category category)
        {
            try
            {
                context.Category.Remove(category);
                var result = await context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                var allCategory = await context.Category.ToListAsync();
                return allCategory;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Category?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Category.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Category category)
        {
            try
            {
                context.Category.Update(category);
                var result = await context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
