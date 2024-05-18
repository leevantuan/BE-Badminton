using Business_Logic_Layer.CourtBLL;
using Data_Transfer_Object.CourtDTO;
using Data_Transfer_Object.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtRepositoryBLL courtRepo;

        public CourtController(ICourtRepositoryBLL courtRepo)
        {
            this.courtRepo = courtRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await courtRepo.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await courtRepo.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourtRequestDTO court)
        {
            return Ok(await courtRepo.CreateAsync(court));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CourtRequestDTO court)
        {
            return Ok(await courtRepo.UpdateAsync(id, court));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await courtRepo.DeleteAsync(id));
        }
    }
}
