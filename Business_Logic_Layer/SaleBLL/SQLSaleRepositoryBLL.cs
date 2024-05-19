using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
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
        public async Task<List<GetSale>> GetAllAsync(int pageNumber, int pageSize, string filterQuery)
        {
            var allSale = await saleRepo.GetAllAsync();

            allSale = allSale.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var skipResult = (pageNumber - 1) * pageSize;

            var list = mapper.Map<List<GetSale>>(allSale.Skip(skipResult).Take(pageSize));

            return list;
        }

        //get by id
        public async Task<GetSale?> GetByIdAsync(Guid id)
        {
            var data = await saleRepo.GetByIdAsync(id);
            return mapper.Map<GetSale?>(data);
        }

        //total
        public async Task<int> TotalPage(double pageSize, string filterQuery)
        {
            var getAll = await saleRepo.GetAllAsync();
            getAll = getAll.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var count = getAll.Count();
            double pageNumber = count / pageSize;
            int result = (int)Math.Ceiling(pageNumber);

            return result;
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
