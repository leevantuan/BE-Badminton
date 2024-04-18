using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLPurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly MyDBContext context;

        public SQLPurchaseOrderRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(PurchaseOrder purchase)
        {
            try
            {
                await context.PurchaseOrder.AddAsync(purchase);
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
        public async Task<bool> DeleteAsync(PurchaseOrder purchase)
        {
            try
            {
                context.PurchaseOrder.Remove(purchase);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {  return true; }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<PurchaseOrder>> GetAllAsync()
        {
            try
            {
                var allPurchase = await context.PurchaseOrder.ToListAsync();

                return allPurchase;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<PurchaseOrder?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await context.PurchaseOrder.FirstOrDefaultAsync(x => x.Id == id);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(PurchaseOrder purchase)
        {
            try
            {
                context.PurchaseOrder.Update(purchase);
                var result = await context.SaveChangesAsync();
                if(result == 1) { return true; }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
