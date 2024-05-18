using Business_Logic_Layer.SupplierBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.SupplierDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepositoryBLL supplierRepo;

        public SupplierController(ISupplierRepositoryBLL supplierRepo)
        {
            this.supplierRepo = supplierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string filterQuery = "")
        {
            return Ok(await supplierRepo.GetAllAsync(pageNumber, pageSize, filterQuery));
        }

        [HttpGet("TotalPage")]
        public async Task<IActionResult> GetTotalPage(double pageSize, string filterQuery = "")
        {
            return Ok(await supplierRepo.TotalPage( pageSize, filterQuery));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await supplierRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierRequestDTO supplier)
        {
            return Ok(await supplierRepo.CreateAsync(supplier));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, SupplierRequestDTO supplier)
        {
            return Ok(await supplierRepo.UpdateAsync(id, supplier));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await supplierRepo.DeleteAsync(id));
        }
    }
}
