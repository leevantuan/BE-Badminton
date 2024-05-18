using Business_Logic_Layer.CategoryBLL;
using Data_Transfer_Object.CategoryDTO;
using Data_Transfer_Object.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepositoryBLL categoryRepo;

        public CategoryController(ICategoryRepositoryBLL categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await categoryRepo.GetAllAsync());
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPanigation(int pageNumber, int pageSize)
        {
            return Ok(await categoryRepo.GetPaginationAsync(pageNumber, pageSize));
        }

        [HttpGet("PageTotal")]
        public async Task<IActionResult> GetTotalPage(double pageSize)
        {
            return Ok(await categoryRepo.TotalPage( pageSize));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await categoryRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestDTO category)
        {
            return Ok(await categoryRepo.CreateAsync(category));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CategoryUpdate category)
        {
            return Ok(await categoryRepo.UpdateAsync(id, category));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await categoryRepo.DeleteAsync(id));
        }
    }
}
