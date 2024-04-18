using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLBillRepository : IBillRepository
    {
        private readonly MyDBContext context;

        public SQLBillRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Bill bill)
        {
            try
            {
                await context.Bill.AddAsync(bill);
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
        public async Task<bool> DeleteAsync(Bill bill)
        {
            try
            {
                context.Bill.Remove(bill);
                var result = await context.SaveChangesAsync();
                if(result == 1)
                {
                    return true ;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<Bill>> GetAllAsync()
        {
            try
            {
                var allBill = await context.Bill.ToListAsync();
                return allBill;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Bill?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Bill.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
