using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.SupplierDTO;

namespace Business_Logic_Layer.SupplierBLL
{
    public interface ISupplierRepositoryBLL
    {
        public Task<List<GetSupplier>> GetAllAsync(GetAllRequestModel request);

        public Task<GetSupplier?> GetByIdAsync(Guid id);

        public Task<bool> CreateAsync(SupplierRequestDTO supplier);

        public Task<bool> UpdateAsync(Guid id, SupplierRequestDTO supplier);

        public Task<bool> DeleteAsync(Guid id);
    }
}
