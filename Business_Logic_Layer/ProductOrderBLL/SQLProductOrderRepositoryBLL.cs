using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.ProductOrderDTO;
using Microsoft.AspNetCore.Identity;

namespace Business_Logic_Layer.ProductOrderBLL
{
    public class SQLProductOrderRepositoryBLL : IProductOrderRepositoryBLL
    {
        private readonly IProductOrderRepository productOrderRepo;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepo;
        private readonly IProductRepository productRepo;
        private readonly UserManager<IdentityUser> userManager;

        public SQLProductOrderRepositoryBLL
            (
            IProductOrderRepository productOrderRepo, 
            IMapper mapper, 
            IOrderRepository orderRepo, 
            IProductRepository productRepo,
            UserManager<IdentityUser> userManager
            )
        {
            this.productOrderRepo = productOrderRepo;
            this.mapper = mapper;
            this.orderRepo = orderRepo;
            this.productRepo = productRepo;
            this.userManager = userManager;
        }

        //create
        public async Task<bool> CreateAsync(ProductOrderRequestDTO productOrder)
        {
            var data = mapper.Map<ProductOrder>(productOrder);
            var quantity = mapper.Map<ChangeQuantity>(data);
            var product = await productRepo.GetByIdAsync(data.ProductId);
            if (product == null)
            {
                return false;
            }
            var change = await productRepo.ReduceQuantityAsync(product, quantity);
            if(change == true)
            {
                return await productOrderRepo.CreateAsync(data);
            }
            return false;
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await productOrderRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }

            var quantity = mapper.Map<ChangeQuantity>(result);
            var product = await productRepo.GetByIdAsync(result.ProductId);
            if (product == null)
            {
                return false;
            }

            var change = await productRepo.InceaseQuantityAsync(product, quantity);
            if (change == true)
            {
                return await productOrderRepo.DeleteAsync(result);
            }
            return false;
        }

        //get all
        public async Task<List<GetProductOrder>> GetAllAsync(GetAllRequestModel request)
        {
            var allProductOrder = await productOrderRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    allProductOrder = mapper.Map<List<ProductOrder>>(request.IsAcsending ? allProductOrder.OrderBy(x => x.CreateAt) : allProductOrder.OrderByDescending(x => x.CreateAt));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetProductOrder>>(allProductOrder.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by id
        public async Task<GetProductOrder?> GetByIdAsync(Guid id)
        {
            var data = await productOrderRepo.GetByIdAsync(id);
            return mapper.Map<GetProductOrder?>(data);
        }

        //get by order
        public async Task<List<GetProductOrderDetail>> GetOrderIdAsync(Guid orderId)
        {
            var result = new List<GetProductOrderDetail>();

            var data = await productOrderRepo.GetAllAsync();
            var productOrderByOrder = data.Where(x => x.OrderId == orderId);

            foreach (var productOrder in productOrderByOrder)
            {
                var order = await orderRepo.GetByIdAsync(productOrder.OrderId);
                var product = await productRepo.GetByIdAsync(productOrder.ProductId);
                var user = await userManager.FindByIdAsync(order.UserId);

                var newProductOrder = mapper.Map<GetProductOrderDetail>(productOrder);
                newProductOrder.ProductName = product.Name;
                newProductOrder.UserName = user.UserName;

                result.Add(newProductOrder);
            }
            return result;
        }

        //get by product
        public async Task<List<GetProductOrderDetail>> GetProductIdAsync(Guid productId)
        {
            var result = new List<GetProductOrderDetail>();

            var data = await productOrderRepo.GetAllAsync();
            var productOrderByProduct = data.Where(x => x.ProductId == productId);

            foreach (var productOrder in productOrderByProduct)
            {
                var order = await orderRepo.GetByIdAsync(productOrder.OrderId);
                var product = await productRepo.GetByIdAsync(productOrder.ProductId);
                var user = await userManager.FindByIdAsync(order.UserId);

                var newProductOrder = mapper.Map<GetProductOrderDetail>(productOrder);
                newProductOrder.ProductName = product.Name;
                newProductOrder.UserName = user.UserName;

                result.Add(newProductOrder);
            }
            return result;
        }
    
        //get total quantity roduct
        public async Task<double> GetFilterTotalQuantityBySaleAsync(Guid productId, FilterDateTimeModel date)
        {
            double total = 0;
            var allProduct = await GetProductIdAsync(productId);
            var filterProduct = allProduct.Where(x => x.CreateAt >= date.StartTimeQuery &&  x.CreateAt <= date.EndTimeQuery);
            foreach (var product in filterProduct)
            {
                total = total + product.Quantity;
            }
            return total;
        }
    
    }
}
