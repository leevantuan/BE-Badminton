using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.CategoryDTO;
using Data_Transfer_Object.GetAll;

namespace Business_Logic_Layer.CategoryBLL
{
    public class SQLCategoryRepositoryBLL : ICategoryRepositoryBLL
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;

        public SQLCategoryRepositoryBLL(ICategoryRepository categoryRepo, IMapper mapper)
        {
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        //create
        public async Task<bool> CreateAsync(CategoryRequestDTO category)
        {
            var data = mapper.Map<Category>(category);
            return await categoryRepo.CreateAsync(data);
        }

        //delete
        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await categoryRepo.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            return await categoryRepo.DeleteAsync(result);
        }

        //get all
        public async Task<List<GetCategory>> GetAllAsync(GetAllRequestModel request)
        {
            var allCategory = await categoryRepo.GetAllAsync();

            if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
            {
                if (request.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Contains lọc phân biệt chữ hoa chữ thường.
                    allCategory = mapper.Map<List<Category>>(allCategory.Where(p => p.Name.ToLowerInvariant().Contains(request.FilterQuery.ToLowerInvariant())));
                }
            }

            if (string.IsNullOrWhiteSpace(request.SortBy) == false)
            {
                if (request.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    allCategory = mapper.Map<List<Category>>(request.IsAcsending ? allCategory.OrderBy(x => x.Name) : allCategory.OrderByDescending(x => x.Name));
                }
            }
            var skipResult = (request.PageNumber - 1) * request.PageSize;

            var list = mapper.Map<List<GetCategory>>(allCategory.Skip(skipResult).Take(request.PageSize));

            return list;
        }

        //get by id
        public async Task<GetCategory?> GetByIdAsync(Guid id)
        {
            var data = await categoryRepo.GetByIdAsync(id);
            return mapper.Map<GetCategory?>(data);
        }

        //update
        public async Task<bool> UpdateAsync(Guid id, CategoryUpdate category)
        {
            var result = mapper.Map<Category>(category);
            result.Id = id;
            return await categoryRepo.UpdateAsync(result);
        }
    }
}
