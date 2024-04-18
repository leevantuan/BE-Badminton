using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLSaleRepository : ISaleRepository
    {
        private readonly MyDBContext context;

        public SQLSaleRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Sale sale)
        {
            try
            {
                await context.Sale.AddAsync(sale);
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
        public async Task<bool> DeleteAsync(Sale sale)
        {
            try
            {
                context.Sale.Remove(sale);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<Sale>> GetAllAsync()
        {
            try
            {
                var allSale = await context.Sale.ToListAsync();
                return allSale;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Sale.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Sale sale)
        {
            try
            {
                context.Sale.Update(sale);
                var result = await context.SaveChangesAsync();
                if (result == 1) { return true; }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
