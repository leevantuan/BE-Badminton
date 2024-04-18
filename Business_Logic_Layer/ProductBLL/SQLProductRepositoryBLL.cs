using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;

namespace Business_Logic_Layer.ProductBLL
{
    public class SQLProductRepositoryBLL : IProductRepositoryBLL
    {
        private readonly IProductRepository productRepo;
        private readonly IMapper mapper;

        public SQLProductRepositoryBLL(IProductRepository productRepo, IMapper mapper)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
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
        public async Task<List<GetProduct>> GetAllAsync(GetAllRequestModel request)
        {
            var allProduct = await productRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allProduct = mapper.Map<List<Product>>(allProduct.Where(p => p.Name.Contains(request.FilterQuery)));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Quantity", StringComparison.OrdinalIgnoreCase))
                {
                    allProduct = mapper.Map<List<Product>>(request.IsAcsending ? allProduct.OrderBy(x => x.Quantity) : allProduct.OrderByDescending(x => x.Quantity));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetProduct>>(allProduct.Skip(skipResult).Take(request.PageSize));

            return list;

        }

        //get by id
        public async Task<GetProduct?> GetByIdAsync(Guid id)
        {
            var data = await productRepo.GetByIdAsync(id);
            return mapper.Map<GetProduct?>(data);
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
    }
}
