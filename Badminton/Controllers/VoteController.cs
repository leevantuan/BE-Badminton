using Business_Logic_Layer.VoteBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteRepositoryBLL voteRepo;

        public VoteController(IVoteRepositoryBLL voteRepo)
        {
            this.voteRepo = voteRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await voteRepo.GetAllAsync(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await voteRepo.GetByIdAsync(id));
        }

        [HttpGet("Product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            return Ok(await voteRepo.GetByProductIdAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(VoteRequestDTO vote)
        {
            return Ok(await voteRepo.CreateAsync(vote));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, VoteRequestDTO vote)
        {
            return Ok(await voteRepo.UpdateAsync(id, vote));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await voteRepo.DeleteAsync(id));
        }
    }
}
