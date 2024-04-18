using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductOrderDTO;

namespace Business_Logic_Layer.ProductOrderBLL
{
    public interface IProductOrderRepositoryBLL
    {
        public Task<List<GetProductOrder>> GetAllAsync(GetAllRequestModel request);

        public Task<GetProductOrder?> GetByIdAsync(Guid id);

        public Task<List<GetProductOrderDetail>> GetOrderIdAsync(Guid orderId);

        public Task<List<GetProductOrderDetail>> GetProductIdAsync(Guid productId);

        public Task<double> GetFilterTotalQuantityBySaleAsync(Guid productId, FilterDateTimeModel date);

        public Task<bool> CreateAsync(ProductOrderRequestDTO productOrder);

        public Task<bool> DeleteAsync(Guid id);
    }
}
