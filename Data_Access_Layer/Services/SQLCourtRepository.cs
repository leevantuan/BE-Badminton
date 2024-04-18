using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLCourtRepository : ICourtRepository
    {
        private readonly MyDBContext context;

        public SQLCourtRepository(MyDBContext context)
        {
            this.context = context;
        }

        //Create
        public async Task<bool> CreateAsync(Court court)
        {
            try
            {
                await context.Court.AddAsync(court);
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

        //Delete
        public async Task<bool> DeleteAsync(Court court)
        {
            try
            {
                context.Court.Remove(court);
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

        //Get all
        public async Task<List<Court>> GetAllAsync()
        {
            try
            {
                var allCourt = await context.Court.ToListAsync();
                return allCourt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get by Id
        public async Task<Court?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await context.Court.FirstOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Update
        public async Task<bool> UpdateAsync(Court court)
        {
            try
            {
                context.Court.Update(court);
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
    }
}
