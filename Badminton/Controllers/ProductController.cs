using Business_Logic_Layer.ProductBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepositoryBLL productRepo;

        public ProductController(IProductRepositoryBLL productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await productRepo.GetAllAsync(request));
        }

        [HttpGet("Category/{categoryId:guid}")]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            return Ok(await productRepo.GetByCategoryId(categoryId));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await productRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestDTO product)
        {
            return Ok(await productRepo.CreateAsync(product));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ProductRequestDTO product)
        {
            return Ok(await productRepo.UpdateAsync(id, product));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await productRepo.DeleteAsync(id));
        }
    }
}
