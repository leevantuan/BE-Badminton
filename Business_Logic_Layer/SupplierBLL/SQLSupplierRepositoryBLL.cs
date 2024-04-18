using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.GetAll;
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
        public async Task<List<GetSupplier>> GetAllAsync(GetAllRequestModel request)
        {
            var allSupplier = await supplierRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allSupplier = mapper.Map<List<Supplier>>(allSupplier.Where(p => p.Name.ToLowerInvariant().Contains(request.FilterQuery.ToLowerInvariant())));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    allSupplier = mapper.Map<List<Supplier>>(request.IsAcsending ? allSupplier.OrderBy(x => x.Name) : allSupplier.OrderByDescending(x => x.Name));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetSupplier>>(allSupplier.Skip(skipResult).Take(request.PageSize));

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
    }
}
