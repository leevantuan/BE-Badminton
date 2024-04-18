using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.OrderDTO;

namespace Business_Logic_Layer.OrderBLL
{
    public interface IOrderRepositoryBLL
    {
        public Task<List<GetOrder>> GetAllAsync(GetAllRequestModel request);

        public Task<GetOrder?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(OrderRequestDTO order);

        public Task<GetOrderDetail> GetProductByOrderDetailAsync(Guid orderId);

        public Task<List<GetOrderDetail>> GetByCustomerIdAsync(string customerId);

        public Task<List<GetOrder>> GetByInDateAsync(FindDateTimeModel date);

        public Task<List<GetOrder>> GetByFilterInDateAsync(FilterDateTimeModel date);

        public Task<double> GetTotalByInDateAsync(FindDateTimeModel date);

        public Task<double> GetTotalByFilterInDateAsync(FilterDateTimeModel date);

        //public Task<bool> UpdateAsync(Guid id, OrderRequestDTO order);

        public Task<bool> ChangeStatusAsync(Guid id, OrderUpdate order);

        public Task<bool> DeleteAsync(Guid id);
    }
}
