using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore; 

namespace Data_Access_Layer.Services
{
    public class SQLProductBillRepository : IProductBillRepository
    {
        private readonly MyDBContext context;

        public SQLProductBillRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(ProductBill productBill)
        {
            try
            {
                await context.ProductBill.AddAsync(productBill);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //delete
        public async Task<bool> DeleteAsync(ProductBill productBill)
        {
            try
            {
                context.ProductBill.Remove(productBill);
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

        //get all
        public async Task<List<ProductBill>> GetAllAsync()
        {
            try
            {
                var allProductBill = await context.ProductBill.ToListAsync();
                return allProductBill;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<ProductBill?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.ProductBill.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by product id
        public async Task<List<ProductBill>> GetByProductIdAsync(Guid productId)
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
