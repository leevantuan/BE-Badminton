using AutoMapper;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Services.Interface;
using Data_Transfer_Object.CategoryDTO;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using System.Collections.Generic;

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
        public async Task<List<GetCategory>> GetPaginationAsync(int pageNumber, int pageSize)
        {
            var allCategory = await categoryRepo.GetAllAsync();
            var skipResult = (pageNumber - 1) * pageSize;

            var result = mapper.Map<List<GetCategory>>(allCategory.Skip(skipResult).Take(pageSize));
            return result;
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
        public async Task<List<GetCategory>> GetAllAsync()
        {
            var allCategory = await categoryRepo.GetAllAsync();
            var result = mapper.Map<List<GetCategory>>(allCategory);

            return result;
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

        //page number total
        public async Task<int> TotalPage(double pageSize)
        {
            var getAll = await categoryRepo.GetAllAsync();

            var count = getAll.Count();
            double pageNumber = count / pageSize;
            int result = (int)Math.Ceiling(pageNumber);

            return result;
        }
    }
}
