using AutoMapper;
using Business_Logic_Layer.ProductBLL;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.PurchaseOrderDTO;

namespace Business_Logic_Layer.PurchaseOrderBLL
{
    public class SQLPurchaseOrderRepositoryBLL : IPurchaseOrderRepositoryBLL
    {
        private readonly IPurchaseOrderRepository purchaseRepo;
        private readonly IMapper mapper;
        private readonly IProductRepositoryBLL productRepo;

        public SQLPurchaseOrderRepositoryBLL(IPurchaseOrderRepository purchaseRepo, IMapper mapper, IProductRepositoryBLL productRepo)
        {
            this.purchaseRepo = purchaseRepo;
            this.mapper = mapper;
            this.productRepo = productRepo;
        }

        //create
        public async Task<bool> CreateAsync(PurchaseOrderRequestDTO purchase)
        {
            var data = mapper.Map<PurchaseOrder>(purchase);
            var result = await purchaseRepo.CreateAsync(data);
            if (result == true)
            {
                var quantity = mapper.Map<ChangeQuantity>(purchase);
                var change = await productRepo.InceaseQuantityAsync(purchase.ProductId, quantity);
                if (change == true)
                {
                    return result;
                }
                return false;
            }
            return result;
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await purchaseRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }

            ChangeQuantity quantity = mapper.Map<ChangeQuantity>(result);

            var change = await productRepo.ReduceQuantityAsync(result.ProductId, quantity);

            if(change == true)
            {
                return await purchaseRepo.DeleteAsync(result);
            }
            return false;
        }

        //get all ...fixxx
        public async Task<List<GetPurchaseOrder>> GetAllAsync(GetAllDateTimeModel request)
        {
            var allPurchase = await purchaseRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false)
            {
                if (request.FilterOn.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    allPurchase = mapper.Map<List<PurchaseOrder>>(allPurchase.Where(p => p.CrateAt == request.FilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    allPurchase = mapper.Map<List<PurchaseOrder>>(request.IsAcsending ? allPurchase.OrderBy(x => x.CrateAt) : allPurchase.OrderByDescending(x => x.CrateAt));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetPurchaseOrder>>(allPurchase.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by id
        public async Task<GetPurchaseOrder?> GetByIdAsync(Guid id)
        {
            var data = await purchaseRepo.GetByIdAsync(id);
            return mapper.Map<GetPurchaseOrder?>(data);
        }

        ////update
        //public async Task<bool> UpdateAsync(Guid id, PurchaseOrderRequestDTO purchase)
        //{
        //    var oldQuantity = new ChangeQuantity();

        //    var dataOld = await purchaseRepo.GetByIdAsync(id);
        //    if (dataOld == null)
        //    {
        //        return false;
        //    }

        //    oldQuantity.Quantity = dataOld.Quantity;

        //    var newQuantity = new ChangeQuantity();
        //    newQuantity.Quantity = purchase.Quantity;

        //    var reduce = await productRepo.ReduceQuantityAsync(purchase.ProductId, oldQuantity);
        //    var incease = await productRepo.InceaseQuantityAsync(purchase.ProductId, newQuantity);

        //    if (reduce == true && incease == true)
        //    {
        //        var data = mapper.Map<PurchaseOrder>(purchase);
        //        data.Id = id;
        //        var result = await purchaseRepo.UpdateAsync(data);
        //        return result;
        //    }

        //    return false;
        //}

        //supplier id
        
        public async Task<List<GetPurchaseOrderDetail>> GetBySupplierIdAsync(Guid supplierId)
        {
            var result = new List<GetPurchaseOrderDetail>();
            var data = await purchaseRepo.GetAllAsync();
            result = mapper.Map<List<GetPurchaseOrderDetail>>(data.Where(x => x.SupplierId == supplierId));
            return result;
        }

        //product id
        public async Task<List<GetPurchaseOrderDetail>> GetByProductIdAsync(Guid productId)
        {
            var result = new List<GetPurchaseOrderDetail>();
            var data = await purchaseRepo.GetAllAsync();
            result = mapper.Map<List<GetPurchaseOrderDetail>>(data.Where(x => x.ProductId == productId));
            return result;
        }
    }
}
