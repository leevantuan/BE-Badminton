using AutoMapper;
using Business_Logic_Layer.ProductBLL;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.PurchaseOrderDTO;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic_Layer.PurchaseOrderBLL
{
    public class SQLPurchaseOrderRepositoryBLL : IPurchaseOrderRepositoryBLL
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IPurchaseOrderRepository purchaseRepo;
        private readonly IMapper mapper;
        private readonly IProductRepositoryBLL productRepo;
        private readonly IProductRepository product;
        private readonly ISupplierRepository supplier;

        public SQLPurchaseOrderRepositoryBLL(UserManager<IdentityUser> userManager, IPurchaseOrderRepository purchaseRepo, IMapper mapper, IProductRepositoryBLL productRepo, IProductRepository product, ISupplierRepository supplier)
        {
            this.userManager = userManager;
            this.purchaseRepo = purchaseRepo;
            this.mapper = mapper;
            this.productRepo = productRepo;
            this.product = product;
            this.supplier = supplier;
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
        public async Task<List<GetPurchaseOrderDetail>> GetAllAsync(int pageNumber, int pageSize, string filterQuery)
        {
            var result = new List<GetPurchaseOrderDetail>();
            var allPurchase = await purchaseRepo.GetAllAsync();

            foreach (var purchase in allPurchase)
            {
                var purchaseCus = mapper.Map<GetPurchaseOrderDetail>(purchase);
                var pro = await product.GetByIdAsync(purchase.ProductId);
                var sup = await supplier.GetByIdAsync(purchase.SupplierId);
                var user = await userManager.FindByIdAsync(purchase.UserId);
                if(pro != null && sup != null)
                {
                    purchaseCus.ProductName = pro.Name;
                    purchaseCus.SupplierName = sup.Name;
                    purchaseCus.UserName = user.UserName;

                    result.Add(purchaseCus);
                }
            }


            result = result.Where(p => p.ProductName.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var skipResult = (pageNumber - 1) * pageSize;

            var list = mapper.Map<List<GetPurchaseOrderDetail>>(result.Skip(skipResult).Take(pageSize));

            return list;
        }

        //get all customer 
        private async Task<List<GetPurchaseOrderDetail>> GetCustom()
        {
            var result = new List<GetPurchaseOrderDetail>();
            var allPurchase = await purchaseRepo.GetAllAsync();

            foreach (var purchase in allPurchase)
            {
                var purchaseCus = mapper.Map<GetPurchaseOrderDetail>(purchase);
                var pro = await product.GetByIdAsync(purchase.ProductId);
                var sup = await supplier.GetByIdAsync(purchase.SupplierId);
                var user = await userManager.FindByIdAsync(purchase.UserId);

                if (pro != null && sup != null)
                {
                    purchaseCus.ProductName = pro.Name;
                    purchaseCus.SupplierName = sup.Name;
                    purchaseCus.UserName = user.UserName;

                    result.Add(purchaseCus);
                }
            }

            return result;
        }
        //total page
        public async Task<int> TotalPage(double pageSize, string filterQuery)
        {
            var getAll = await GetCustom();
            getAll = getAll.Where(p => p.ProductName.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var count = getAll.Count();
            double pageNumber = count / pageSize;
            int result = (int)Math.Ceiling(pageNumber);

            return result;
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

        //Purchase id
        
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
