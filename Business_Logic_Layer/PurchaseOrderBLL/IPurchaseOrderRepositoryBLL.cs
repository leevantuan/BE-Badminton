using Data_Transfer_Object.PurchaseOrderDTO;

namespace Business_Logic_Layer.PurchaseOrderBLL
{
    public interface IPurchaseOrderRepositoryBLL
    {
        public Task<List<GetPurchaseOrderDetail>> GetAllAsync(int pageNumber, int pageSize, string filterQuery);

        public Task<int> TotalPage(double pageSize, string filterQuery);

        public Task<GetPurchaseOrder?> GetByIdAsync(Guid id);

        public Task<List<GetPurchaseOrderDetail>> GetBySupplierIdAsync(Guid supplierId);

        public Task<List<GetPurchaseOrderDetail>> GetByProductIdAsync(Guid productId);

        public Task<bool> CreateAsync(PurchaseOrderRequestDTO purchase);

        //public Task<bool> UpdateAsync(Guid id, PurchaseOrderRequestDTO purchase);

        public Task<bool> DeleteAsync(Guid id);
    }
}
