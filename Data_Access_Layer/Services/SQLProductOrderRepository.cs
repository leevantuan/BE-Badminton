using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLProductOrderRepository : IProductOrderRepository
    {
        private readonly MyDBContext context;

        public SQLProductOrderRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(ProductOrder productOrder)
        {
            try
            {
                await context.ProductOrder.AddAsync(productOrder);
                var result = await context.SaveChangesAsync();
                if(result == 1)
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
        public async Task<bool> DeleteAsync(ProductOrder productOrder)
        {
            try
            {
                context.ProductOrder.Remove(productOrder);
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
        public async Task<List<ProductOrder>> GetAllAsync()
        {
            try
            {
                var allProductOrder = await context.ProductOrder.ToListAsync();
                return allProductOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<ProductOrder?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.ProductOrder.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by product id
        public async Task<List<ProductOrder>> GetByProductIdAsync(Guid productId)
        {
            try
            {
                var getAll = await GetAllAsync();
                var data = getAll.Where(x => x.ProductId == productId);
                return data.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
