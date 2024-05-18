using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using Data_Transfer_Object.SupplierDTO;

namespace Business_Logic_Layer.SupplierBLL
{
    public class SQLSupplierRepositoryBLL : ISupplierRepositoryBLL
    {
        private readonly ISupplierRepository supplierRepo;
        private readonly IMapper mapper;

        public SQLSupplierRepositoryBLL(ISupplierRepository supplierRepo, IMapper mapper)
        {
            this.supplierRepo = supplierRepo;
            this.mapper = mapper;
        }

        //create
        public async Task<bool> CreateAsync(SupplierRequestDTO purchase)
        {
            var data = mapper.Map<Supplier>(purchase);
            return await supplierRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await supplierRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await supplierRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetSupplier>> GetAllAsync(int pageNumber, int pageSize, string filterQuery)
        {
            var allSupplier = await supplierRepo.GetAllAsync();
            allSupplier = allSupplier.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var skipResult = (pageNumber - 1) * pageSize;

            var list = mapper.Map<List<GetSupplier>>(allSupplier.Skip(skipResult).Take(pageSize));

            return list;

        }

        //Get by id
        public async Task<GetSupplier?> GetByIdAsync(Guid id)
        {
            var data = await supplierRepo.GetByIdAsync(id);
            return mapper.Map<GetSupplier>(data);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, SupplierRequestDTO purchase)
        {
            var result = mapper.Map<Supplier>(purchase);
            result.Id = id;
            return await supplierRepo.UpdateAsync(result);
        }

        public async Task<int> TotalPage(double pageSize, string filterQuery)
        {
            var getAll = await supplierRepo.GetAllAsync();
            getAll = getAll.Where(p => p.Name.ToLowerInvariant().Contains(filterQuery.ToLowerInvariant())).ToList();

            var count = getAll.Count();
            double pageNumber = count / pageSize;
            int result = (int)Math.Ceiling(pageNumber);

            return result;
        }

    }
}
