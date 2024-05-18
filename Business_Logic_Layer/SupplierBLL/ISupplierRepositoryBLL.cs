using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.SupplierDTO;

namespace Business_Logic_Layer.SupplierBLL
{
    public interface ISupplierRepositoryBLL
    {
        public Task<List<GetSupplier>> GetAllAsync(int pageNumber, int pageSize, string filterQuery);

        public Task<int> TotalPage(double pageSize, string filterQuery);

        public Task<GetSupplier?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(SupplierRequestDTO supplier);

        public Task<bool> UpdateAsync(Guid id, SupplierRequestDTO supplier);

        public Task<bool> DeleteAsync(Guid id);
    }
}
