using Business_Logic_Layer.PurchaseOrderBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.PurchaseOrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderRepositoryBLL purchaseOrderRepo;

        public PurchaseOrderController(IPurchaseOrderRepositoryBLL purchaseOrderRepo)
        {
            this.purchaseOrderRepo = purchaseOrderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string filterQuery = "")
        {
            return Ok(await purchaseOrderRepo.GetAllAsync(pageNumber, pageSize, filterQuery));
        }

        [HttpGet("TotalPage")]
        public async Task<IActionResult> GetTotalPage(double pageSize, string filterQuery = "")
        {
            return Ok(await purchaseOrderRepo.TotalPage(pageSize, filterQuery));
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await purchaseOrderRepo.GetByIdAsync(id));
        }

        [HttpGet("Supplier/{id:guid}")]
        public async Task<IActionResult> GetBySupplierId(Guid id)
        {
            return Ok(await purchaseOrderRepo.GetBySupplierIdAsync(id));
        }

        [HttpGet("Product/{id:guid}")]
        public async Task<IActionResult> GetByProductId(Guid id)
        {
            return Ok(await purchaseOrderRepo.GetByProductIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PurchaseOrderRequestDTO purchase)
        {
            return Ok(await purchaseOrderRepo.CreateAsync(purchase));
        }

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> Update(Guid id, PurchaseOrderRequestDTO purchase)
        //{
        //    return Ok(await purchaseOrderRepo.UpdateAsync(id, purchase));
        //}

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await purchaseOrderRepo.DeleteAsync(id));
        }
    }
}
