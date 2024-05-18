using Business_Logic_Layer.BillBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.BillDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillRepositoryBLL billRepo;

        public BillController(IBillRepositoryBLL billRepo)
        {
            this.billRepo = billRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllDateTimeModel request)
        {
            return Ok(await billRepo.GetAllAsync(request));
        }

        [HttpGet("GetTotalByFilterInDate")]
        public async Task<IActionResult> GetTotalByFilterInDate(FilterDateTimeModel date)
        {
            return Ok(await billRepo.GetTotalByFilterInDateAsync(date));
        }

        [HttpGet("GetTotalByInDate")]
        public async Task<IActionResult> GetTotalByInDate(FindDateTimeModel date)
        {
            return Ok(await billRepo.GetTotalByInDateAsync(date));
        }

        [HttpGet("GetByFilterInDate")]
        public async Task<IActionResult> GetByFilterInDate(FilterDateTimeModel date)
        {
            return Ok(await billRepo.GetByFilterInDateAsync(date));
        }

        [HttpGet("GetByInDate")]
        public async Task<IActionResult> GetByInDate(FindDateTimeModel date)
        {
            return Ok(await billRepo.GetByInDateAsync(date));
        }

        [HttpGet("Sale/{saleId:guid}")]
        public async Task<IActionResult> GetBySaleId(Guid saleId)
        {
            return Ok(await billRepo.GetBySaleIdAsync(saleId));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await billRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(BillRequestDTO bill)
        {
            return Ok(await billRepo.CreateAsync(bill));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(await billRepo.DeleteAsync(id));
        }
    }
}
