using Business_Logic_Layer.SaleBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.SaleDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepositoryBLL saleRepo;

        public SaleController(ISaleRepositoryBLL saleRepo)
        {
            this.saleRepo = saleRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await saleRepo.GetAllAsync(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await saleRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleRequestDTO sale)
        {
            return Ok(await saleRepo.CreateAsync(sale));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, SaleRequestDTO sale)
        {
            return Ok(await saleRepo.UpdateAsync(id, sale));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await saleRepo.DeleteAsync(id));
        }
    }
}
