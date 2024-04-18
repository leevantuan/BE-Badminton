using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.SaleDTO;

namespace Business_Logic_Layer.SaleBLL
{
    public class SQLSaleRepositoryBLL : ISaleRepositoryBLL
    {
        private readonly ISaleRepository saleRepo;
        private readonly IMapper mapper;

        public SQLSaleRepositoryBLL(ISaleRepository saleRepo, IMapper mapper)
        {
            this.saleRepo = saleRepo;
            this.mapper = mapper;
        }

        //create
        public async Task<bool> CreateAsync(SaleRequestDTO sale)
        {
            var data = mapper.Map<Sale>(sale);
            return await saleRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await saleRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await saleRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetSale>> GetAllAsync(GetAllRequestModel request)
        {
            var allSale = await saleRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allSale = mapper.Map<List<Sale>>(allSale.Where(p => p.Name.ToLowerInvariant().Contains(request.FilterQuery.ToLowerInvariant())));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    allSale = mapper.Map<List<Sale>>(request.IsAcsending ? allSale.OrderBy(x => x.IsStatus) : allSale.OrderByDescending(x => x.IsStatus));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetSale>>(allSale.Skip(skipResult).Take(request.PageSize));

            return list;

        }

        //get by id
        public async Task<GetSale?> GetByIdAsync(Guid id)
        {
            var data = await saleRepo.GetByIdAsync(id);
            return mapper.Map<GetSale?>(data);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, SaleRequestDTO sale)
        {
            var result = mapper.Map<Sale>(sale);
            result.Id = id;
            return await saleRepo.UpdateAsync(result);
        }
    }
}
