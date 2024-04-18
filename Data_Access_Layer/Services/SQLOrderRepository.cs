using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.OrderDTO;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Services
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly MyDBContext context;

        public SQLOrderRepository(MyDBContext context)
        {
            this.context = context;
        }

        //create
        public async Task<bool> CreateAsync(Order order)
        {
            try
            {
                await context.Order.AddAsync(order);
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
        public async Task<bool> DeleteAsync(Order order)
        {
            try
            {
                context.Order.Remove(order);
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
        public async Task<List<Order>> GetAllAsync()
        {
            try
            {
                var allOrder = await context.Order.ToListAsync();
                return allOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get by id
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            try
            {
                var data = await context.Order.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //update
        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {
                context.Order.Update(order);
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

        //change status
        public async Task<bool> ChangeStatusAsync(Guid id, OrderUpdate order)
        {
            try
            {
                var data = await context.Order.FindAsync(id);
                if (data == null)
                {
                    return false;
                }

                data.IsStatus = order.IsStatus;
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
