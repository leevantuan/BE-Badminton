using AutoMapper;
using Azure.Core;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business_Logic_Layer.ProductBLL
{
    public class SQLProductRepositoryBLL : IProductRepositoryBLL
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;
        private readonly IProductOrderRepository productOrderRepo;
        private readonly IProductBillRepository productBillRepo;

        public SQLProductRepositoryBLL(IProductRepository productRepo, IMapper mapper, IProductOrderRepository productOrderRepo, IProductBillRepository productBillRepo)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
            this.productOrderRepo = productOrderRepo;
            this.productBillRepo = productBillRepo;
        }

        //change
        public async Task<bool> InceaseQuantityAsync(Guid id, ChangeQuantity quantity)
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            return await productRepo.InceaseQuantityAsync(product, quantity);
        }

        public async Task<bool> ReduceQuantityAsync(Guid id, ChangeQuantity quantity)
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            return await productRepo.ReduceQuantityAsync(product, quantity);
        }

        //create
        public async Task<bool> CreateAsync(ProductRequestDTO product)
        {
            var data = mapper.Map<Product>(product);
            return await productRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }
            return await productRepo.DeleteAsync(product);
        }

        //get all
        public async Task<List<GetProduct>> GetAllAsync(int pageNumber, int pageSize, string filterQuery)
        {
            var result = new List<GetProduct>();
            var allProduct = await productRepo.GetAllAsync();
            allProduct = allProduct.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            foreach (var product in allProduct)
            {
                double totalOrder = 0;
                double totalBill = 0;

                var resProduct = mapper.Map<GetProduct>(product);
                var listOrdel = await productOrderRepo.GetByProductIdAsync(product.Id);
                var listBill = await productBillRepo.GetByProductIdAsync(product.Id);

                foreach (var order in listOrdel)
                {
                    totalOrder = totalOrder + order.Quantity;
                }

                foreach (var bill in listBill)
                {
                    totalBill = totalBill + bill.Quantity;
                }
                resProduct.SoldQuantity = totalBill + totalOrder;

                result.Add(resProduct);
            }

            var skipResult = (pageNumber - 1) * pageSize;

            var list = mapper.Map<List<GetProduct>>(result.Skip(skipResult).Take(pageSize));

            return list;

        }

        //get by id
        public async Task<GetProduct?> GetByIdAsync(Guid id)
        {
            var data = await productRepo.GetByIdAsync(id);

            double totalOrder = 0;
            double totalBill = 0;

            var resProduct = mapper.Map<GetProduct>(data);
            var listOrdel = await productOrderRepo.GetByProductIdAsync(data.Id);
            var listBill = await productBillRepo.GetByProductIdAsync(data.Id);

            foreach (var order in listOrdel)
            {
                totalOrder = totalOrder + order.Quantity;
            }

            foreach (var bill in listBill)
            {
                totalBill = totalBill + bill.Quantity;
            }
            resProduct.SoldQuantity = totalBill + totalOrder;

            return resProduct;
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, ProductRequestDTO product)
        {
            Product newProduct = mapper.Map<Product>(product);
            newProduct.Id = id;
            return await productRepo.UpdateAsync(newProduct);
        }

        //Get category id
        public async Task<List<GetProduct>> GetByCategoryId(Guid categoryId)
        {
            var result = new List<GetProduct>();
            var listProduct = await productRepo.GetAllAsync();
            result = mapper.Map<List<GetProduct>>(listProduct.Where(p => p.CategoryId == categoryId));
            return result;
        }

        public async Task<int> TotalPage(double pageSize, string filterQuery)
        {
            var getAll = await productRepo.GetAllAsync();
            getAll = getAll.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var count = getAll.Count();
            double pageNumber = count / pageSize;
            int result = (int)Math.Ceiling(pageNumber);

            return result;

        }
    }
}
