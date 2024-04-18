using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly MyDBContext context;

        public SQLBookingRepository(MyDBContext context)
        {
            this.context = context;
        }

        //change status
        public async Task<bool> ChangeStatusAsync(Guid id, bool isStatus)
        {
            try
            {
                var booking = await context.Booking.FirstOrDefaultAsync(x => x.Id == id);
                if (booking == null)
                {
                    return false;
                }
                booking.IsStatus = isStatus;
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

        //Create
        public async Task<bool> CreateAsync(Booking booking)
        {
            try
            {
               await context.Booking.AddAsync(booking);
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
        public async Task<bool> DeleteAsync(Booking booking)
        {
            try
            {
                context.Booking.Remove(booking);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get all
        public async Task<List<Booking>> GetAllAsync()
        {
            try
            {
                var allbooking = await context.Booking.ToListAsync();
                return allbooking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get  by id
        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await context.Booking.FirstOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Update
        public async Task<bool> UpdateAsync(Booking booking)
        {
            try
            {
                context.Booking.Update(booking);
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
