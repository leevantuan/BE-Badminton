using Business_Logic_Layer.ProductBillBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductBillDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBillController : ControllerBase
    {
        private readonly IProductBillRepositoryBLL productBillRepo;

        public ProductBillController(IProductBillRepositoryBLL productBillRepo)
        {
            this.productBillRepo = productBillRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await productBillRepo.GetAllAsync(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await productBillRepo.GetByIdAsync(id));
        }

        [HttpGet("Bill/{billId:guid}")]
        public async Task<IActionResult> GetByBillId(Guid billId)
        {
            return Ok(await productBillRepo.GetBillIdAsync(billId));
        }

        [HttpGet("TotalQuantityProduct/{productId:guid}")]
        public async Task<IActionResult> GetTotalQuantityProduct(Guid productId, FilterDateTimeModel date)
        {
            return Ok(await productBillRepo.GetFilterTotalQuantityBySaleAsync(productId, date));
        }

        [HttpGet("Product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            return Ok(await productBillRepo.GetProductIdAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductBillRequestDTO productBill)
        {
            return Ok(await productBillRepo.CreateAsync(productBill));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await productBillRepo.DeleteAsync(id));
        }
    }
}
