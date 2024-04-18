using Business_Logic_Layer.ProductOrderBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.ProductOrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrderController : ControllerBase
    {
        private readonly IProductOrderRepositoryBLL productRepo;

        public ProductOrderController(IProductOrderRepositoryBLL productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await productRepo.GetAllAsync(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await productRepo.GetByIdAsync(id));
        }

        [HttpGet("Order/{orderId:guid}")]
        public async Task<IActionResult> GetOrderId(Guid orderId)
        {
            return Ok(await productRepo.GetOrderIdAsync(orderId));
        }

        [HttpGet("Product/{productId:guid}")]
        public async Task<IActionResult> GetProductId(Guid productId)
        {
            return Ok(await productRepo.GetProductIdAsync(productId));
        }

        [HttpGet("TotalQuantityProduct/{productId:guid}")]
        public async Task<IActionResult> GetTotalQuantityProduct(Guid productId, FilterDateTimeModel date)
        {
            return Ok(await productRepo.GetFilterTotalQuantityBySaleAsync(productId, date));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductOrderRequestDTO productOrder)
        {
            return Ok(await productRepo.CreateAsync(productOrder));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await productRepo.DeleteAsync(id));
        }
    }
}
