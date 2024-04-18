using AutoMapper;
using Business_Logic_Layer.ProductOrderBLL;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.OrderDTO;

namespace Business_Logic_Layer.OrderBLL
{
    public class SQLOrderRepositoryBLL : IOrderRepositoryBLL
    {
        private readonly IProductRepository productRepo;
        private readonly IOrderRepository orderRepo;
        private readonly IMapper mapper;
        private readonly IProductOrderRepositoryBLL productOrderRepoBLL;
        private readonly IProductOrderRepository productOrderRepo;

        public SQLOrderRepositoryBLL(IProductRepository productRepo, IOrderRepository orderRepo, IMapper mapper, IProductOrderRepositoryBLL productOrderRepoBLL, IProductOrderRepository productOrderRepo)
        {
            this.productRepo = productRepo;
            this.orderRepo = orderRepo;
            this.mapper = mapper;
            this.productOrderRepoBLL = productOrderRepoBLL;
            this.productOrderRepo = productOrderRepo;
        }

        //create
        public async Task<bool> CreateAsync(OrderRequestDTO order)
        {
            var data = mapper.Map<Order>(order);
            return await orderRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await orderRepo.GetByIdAsync(id);
            if (result == null || result.IsStatus == true)
            {
                return false;
            }
            var productOrders = await productOrderRepo.GetAllAsync();
            if (productOrders == null)
            {
                return false;
            }
            var productOrderByOrder = productOrders.Where(x => x.OrderId == id);
            foreach (var productOrder in productOrderByOrder)
            {
                await productOrderRepoBLL.DeleteAsync(productOrder.Id);
            }

            return await orderRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetOrder>> GetAllAsync(GetAllRequestModel request)
        {
            var allOrder = await orderRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    allOrder = mapper.Map<List<Order>>(allOrder.Where(p => p.UserId == request.FilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("IsStatus", StringComparison.OrdinalIgnoreCase))
                {
                    allOrder = mapper.Map<List<Order>>(request.IsAcsending ? allOrder.OrderBy(x => x.IsStatus) : allOrder.OrderByDescending(x => x.IsStatus));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetOrder>>(allOrder.Skip(skipResult).Take(request.PageSize));

            return list;

        }

        //get by id
        public async Task<GetOrder?> GetByIdAsync(Guid id)
        {
            var data = await orderRepo.GetByIdAsync(id);
            return mapper.Map<GetOrder?>(data);
        }

        //get by customer detail
        public async Task<List<GetOrderDetail>> GetByCustomerIdAsync(string customerId)
        {
            var result = new List<GetOrderDetail>();
            var allOrders = await orderRepo.GetAllAsync();
            var orderByCustomer = allOrders.Where(x => x.UserId == customerId);
            foreach (var order in orderByCustomer)
            {
                var data = mapper.Map<GetOrderDetail>(order);
                var product = await GetProductByOrderDetail(order.Id);
                data.ProductDetails = product;

                result.Add(data);
            }
            return result;
        }

        ////update
        //public async Task<bool> UpdateAsync(Guid id, OrderUpdate order)
        //{
        //    var result = mapper.Map<Order>(order);
        //    result.Id = id;
        //    return await orderRepo.UpdateAsync(result);
        //}

        //change status
        public async Task<bool> ChangeStatusAsync(Guid id, OrderUpdate order)
        {
            return await orderRepo.ChangeStatusAsync(id, order);
        }

        //get by in date
        public async Task<List<GetOrder>> GetByInDateAsync(FindDateTimeModel date)
        {
            try
            {
                var data = await orderRepo.GetAllAsync();
                return mapper.Map<List<GetOrder>>(data.Where(x => x.CreateAt == date.FindQuery));
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get filter date
        public async Task<List<GetOrder>> GetByFilterInDateAsync(FilterDateTimeModel date)
        {
            try
            {
                var data = await orderRepo.GetAllAsync();
                return mapper.Map<List<GetOrder>>(data.Where(x => x.CreateAt >= date.StartTimeQuery && x.CreateAt <= date.EndTimeQuery));
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get total in date
        public async Task<double> GetTotalByInDateAsync(FindDateTimeModel date)
        {
            try
            {
                double total = 0;
                var data = await GetByInDateAsync(date);
                foreach (var item in data)
                {
                    total = total + item.Total;
                }
                return total;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get total filter date
        public async Task<double> GetTotalByFilterInDateAsync(FilterDateTimeModel date)
        {
            try
            {
                double total = 0;
                var data = await GetByFilterInDateAsync(date);
                foreach (var item in data)
                {
                    total = total + item.Total;
                }
                return total;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetOrderDetail> GetProductByOrderDetailAsync(Guid orderId)
        {
            var order = await orderRepo.GetByIdAsync(orderId);

            var result = mapper.Map<GetOrderDetail>(order);
            var product = await GetProductByOrderDetail(order.Id);
            result.ProductDetails = product;

            return result;
        }

        //product detail
        private async Task<List<ProductDetail>> GetProductByOrderDetail(Guid orderId)
        {
            var result = new List<ProductDetail>();
            var getAllProductOrder = await productOrderRepo.GetAllAsync();
            var getByOrder = getAllProductOrder.Where(x => x.OrderId == orderId);
            foreach (var productOrder in getByOrder)
            {
                var data = new ProductDetail();
                var product = await productRepo.GetByIdAsync(productOrder.ProductId);
                data.Id = product.Id;
                data.Name = product.Name;
                data.ImgLink = product.ImgLink;
                data.Price = product.PriceSale;
                data.Quantity = productOrder.Quantity;
                data.Total = product.PriceSale * productOrder.Quantity;

                result.Add(data);
            }
            return result;
        }

    }
}
