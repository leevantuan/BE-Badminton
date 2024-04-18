using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductBillDTO;
using Data_Transfer_Object.ProductDTO;

namespace Business_Logic_Layer.ProductBillBLL
{
    public class SQLProductBillRepositoryBLL : IProductBillRepositoryBLL
    {
        private readonly IProductBillRepository productBillRepo;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepo;

        public SQLProductBillRepositoryBLL(
            IProductBillRepository productBillRepo, 
            IMapper mapper,
            IProductRepository productRepo
            )
        {
            this.productBillRepo = productBillRepo;
            this.mapper = mapper;
            this.productRepo = productRepo;
        }

        //create
        public async Task<bool> CreateAsync(ProductBillRequestDTO productBill)
        {
            var data = mapper.Map<ProductBill>(productBill);
            var quantity = mapper.Map<ChangeQuantity>(productBill);
            var product = await productRepo.GetByIdAsync(productBill.ProductId);
            if (product == null)
            {
                return false;
            }   
            var change = await productRepo.ReduceQuantityAsync(product, quantity);
            if(change == true)
            {
                return await productBillRepo.CreateAsync(data);
            }
            return false;
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await productBillRepo.GetByIdAsync(id);
            if(result == null)
            {
                return false;
            }
            return await productBillRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetProductBill>> GetAllAsync(GetAllRequestModel request)
        {
            var allProductBill = await productBillRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    allProductBill = mapper.Map<List<ProductBill>>(request.IsAcsending ? allProductBill.OrderBy(x => x.CreateAt) : allProductBill.OrderByDescending(x => x.CreateAt));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetProductBill>>(allProductBill.Skip(skipResult).Take(request.PageSize));

            return list;

        }

        //get by id
        public async Task<GetProductBill?> GetByIdAsync(Guid id)
        {
            var data = await productBillRepo.GetByIdAsync(id);
            return mapper.Map<GetProductBill?>(data);
        }

        //get product bill by bill
        public async Task<List<GetProductBillDetail>> GetBillIdAsync(Guid billId)
        {
            var result = new List<GetProductBillDetail>();

            var data = await productBillRepo.GetAllAsync();
            var productBillByOrder = data.Where(x => x.BillId == billId);

            foreach (var productBill in productBillByOrder)
            {
                var product = await productRepo.GetByIdAsync(productBill.ProductId);

                var newProductOrder = mapper.Map<GetProductBillDetail>(productBill);
                newProductOrder.ProductName = product.Name;

                result.Add(newProductOrder);
            }
            return result;
        }

        //get product bill by product
        public async Task<List<GetProductBillDetail>> GetProductIdAsync(Guid productId)
        {
            var result = new List<GetProductBillDetail>();

            var data = await productBillRepo.GetAllAsync();
            var productBillByProduct = data.Where(x => x.ProductId == productId);

            foreach (var productBill in productBillByProduct)
            {
                var product = await productRepo.GetByIdAsync(productBill.ProductId);

                var newProductOrder = mapper.Map<GetProductBillDetail>(productBill);
                newProductOrder.ProductName = product.Name;

                result.Add(newProductOrder);
            }
            return result;
        }

        //get total quantity product
        public async Task<double> GetFilterTotalQuantityBySaleAsync(Guid productId, FilterDateTimeModel date)
        {
            double total = 0;
            var allProduct = await GetProductIdAsync(productId);
            var filterProduct = allProduct.Where(x => x.CreateAt >= date.StartTimeQuery && x.CreateAt <= date.EndTimeQuery);
            foreach (var product in filterProduct)
            {
                total = total + product.Quantity;
            }
            return total;
        }

        //update
        //public async Task<bool> UpdateAsync(Guid id, ProductBillUpdate productBill)
        //{
        //    return await productBillRepo.UpdateAsync(id, productBill);
        //}
    }
}
