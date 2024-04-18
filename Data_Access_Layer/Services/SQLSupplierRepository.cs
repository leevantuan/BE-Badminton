using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLSupplierRepository : ISupplierRepository
    {
        private readonly MyDBContext context;

        public SQLSupplierRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Supplier supplier)
        {
            try
            {
                await context.Supplier.AddAsync(supplier);
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
        public async Task<bool> DeleteAsync(Supplier supplier)
        {
            try
            {
                context.Supplier.Remove(supplier);
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
        public async Task<List<Supplier>> GetAllAsync()
        {
            try
            {
                var result = await context.Supplier.ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await context.Supplier.FirstOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Supplier supplier)
        {
            try
            {
                context.Supplier.Update(supplier);
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
