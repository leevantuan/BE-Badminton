using Business_Logic_Layer.OrderBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.OrderDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepositoryBLL orderRepo;

        public OrderController(IOrderRepositoryBLL orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await orderRepo.GetAllAsync(request));
        }

        [HttpGet("GetTotalByFilterInDate")]
        public async Task<IActionResult> GetTotalByFilterInDate(FilterDateTimeModel date)
        {
            return Ok(await orderRepo.GetTotalByFilterInDateAsync(date));
        }

        [HttpGet("GetTotalByInDate")]
        public async Task<IActionResult> GetTotalByInDate(FindDateTimeModel date)
        {
            return Ok(await orderRepo.GetTotalByInDateAsync(date));
        }

        [HttpGet("GetByFilterInDate")]
        public async Task<IActionResult> GetByFilterInDate(FilterDateTimeModel date)
        {
            return Ok(await orderRepo.GetByFilterInDateAsync(date));
        }

        [HttpGet("GetByInDate")]
        public async Task<IActionResult> GetByInDate(FindDateTimeModel date)
        {
            return Ok(await orderRepo.GetByInDateAsync(date));
        }
        
        [HttpGet("Customer/{customerId}")]
        public async Task<IActionResult> GetById(string customerId)
        {
            return Ok(await orderRepo.GetByCustomerIdAsync(customerId));
        }

        [HttpGet("ProductDetail/{orderId:guid}")]
        public async Task<IActionResult> GetProductByOrderDetail(Guid orderId)
        {
            return Ok(await orderRepo.GetProductByOrderDetailAsync(orderId));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await orderRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequestDTO order)
        {
            return Ok(await orderRepo.CreateAsync(order));
        }

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> Update(Guid id, OrderUpdate order)
        //{
        //    return Ok(await orderRepo.UpdateAsync(id, order));
        //}

        [HttpPut("ChangeStatus/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, OrderUpdate order)
        {
            return Ok(await orderRepo.ChangeStatusAsync(id, order));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await orderRepo.DeleteAsync(id));
        }
    }
}
