using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.SaleDTO;

namespace Business_Logic_Layer.SaleBLL
{
    public interface ISaleRepositoryBLL
    {
        public Task<List<GetSale>> GetAllAsync(int pageNumber, int pageSize, string filterQuery);

        public Task<int> TotalPage(double pageSize, string filterQuery);

        public Task<GetSale?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(SaleRequestDTO sale);

        public Task<bool> UpdateAsync(Guid id, SaleRequestDTO sale);

        public Task<bool> DeleteAsync(Guid id);
    }
}
