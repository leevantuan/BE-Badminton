using Data_Transfer_Object.BillDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.BillBLL
{
    public interface IBillRepositoryBLL
    {
        public Task<List<GetBill>> GetAllAsync(GetAllDateTimeModel request);

        public Task<GetBill?> GetByIdAsync(Guid id);

        public Task<List<GetBill>> GetByInDateAsync(FindDateTimeModel date);

        public Task<List<GetBill>> GetByFilterInDateAsync(FilterDateTimeModel date);

        public Task<List<GetBill>> GetBySaleIdAsync(Guid saleId);

        public Task<double> GetTotalByInDateAsync(FindDateTimeModel date);

        public Task<double> GetTotalByFilterInDateAsync(FilterDateTimeModel date);

        public Task<bool> CreateAsync(BillRequestDTO bill);

        public Task<bool> DeleteAsync(Guid id);
    }
}
