using Data_Access_Layer.Entities;
using Data_Transfer_Object.OrderDTO;

namespace Data_Access_Layer.Services.Interface
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllAsync();

        public Task<Order?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(Order order);

        public Task<bool> UpdateAsync(Order order);

        public Task<bool> ChangeStatusAsync(Guid id, OrderUpdate order);

        public Task<bool> DeleteAsync(Order order);
    }
}
