using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductBillDTO;

namespace Business_Logic_Layer.ProductBillBLL
{
    public interface IProductBillRepositoryBLL
    {
        public Task<List<GetProductBill>> GetAllAsync(GetAllRequestModel request);

        public Task<GetProductBill?> GetByIdAsync(Guid id);

        public Task<List<GetProductBillDetail>> GetBillIdAsync(Guid billId);

        public Task<List<GetProductBillDetail>> GetProductIdAsync(Guid productId);

        public Task<double> GetFilterTotalQuantityBySaleAsync(Guid productId, FilterDateTimeModel date);

        public Task<bool> CreateAsync(ProductBillRequestDTO productBill);

        //public Task<bool> UpdateAsync(Guid id, ProductBillUpdate productBill);

        public Task<bool> DeleteAsync(Guid id);       
    }
}
