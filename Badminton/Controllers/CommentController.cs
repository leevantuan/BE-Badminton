using Business_Logic_Layer.CommentBLL;
using Data_Transfer_Object.GetAll;
using Data_Transfer_Object.VoteDTO;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepositoryBLL commentRepo;

        public CommentController(ICommentRepositoryBLL commentRepo)
        {
            this.commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            return Ok(await commentRepo.GetAllAsync(request));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await commentRepo.GetByIdAsync(id));
        }

        [HttpGet("Product/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            return Ok(await commentRepo.GetByProductIdAsync(productId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentRequestDTO comment)
        {
            return Ok(await commentRepo.CreateAsync(comment));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, CommentRequestDTO comment)
        {
            return Ok(await commentRepo.UpdateAsync(id, comment));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await commentRepo.DeleteAsync(id));
        }
    }
}
