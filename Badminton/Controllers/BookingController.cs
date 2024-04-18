using Business_Logic_Layer.BookingBLL;
using Data_Transfer_Object.BookingDTO;
using Data_Transfer_Object.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepositoryBLL bookingRepo;

        public BookingController(IBookingRepositoryBLL bookingRepo)
        {
            this.bookingRepo = bookingRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllDateTimeModel request)
        {
            return Ok(await bookingRepo.GetAllAsync(request));
        }

        [HttpGet("GetByFilterInDate")]
        public async Task<IActionResult> GetByFilterInDate(FilterDateTimeModel date)
        {
            return Ok(await bookingRepo.GetByFilterInDateAsync(date));
        }

        [HttpGet("GetByInDate")]
        public async Task<IActionResult> GetByInDate(FindDateTimeModel date)
        {
            return Ok(await bookingRepo.GetByInDateAsync(date));
        }

        [HttpGet("CourtInDate/{courtId:Guid}")]
        public async Task<IActionResult> GetByCourtInDate(Guid courtId, FindDateTimeModel date)
        {
            return Ok(await bookingRepo.GetByCourtInDateAsync(courtId, date));
        }
        
        [HttpGet("Customer/{customerId:Guid}")]
        public async Task<IActionResult> GetByCustomerId(string customerId)
        {
            return Ok(await bookingRepo.GetByCustomerIdAsync(customerId));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        { 
            return Ok(await bookingRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingRequestDTO booking)
        {
            return Ok(await bookingRepo.CreateAsync(booking));
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, BookingRequestDTO booking)
        {
            return Ok(await bookingRepo.UpdateAsync(id,booking));
        }

        [HttpPut("ChangeStatus/{id:Guid}")]
        public async Task<IActionResult> ChangeStatus(Guid id, ChangeStatus isStatus)
        {
            return Ok(await bookingRepo.ChangeStatusAsync(id, isStatus));
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await bookingRepo.DeleteAsync(id));
        }
    }
}
