using Business_Logic_Layer.AuthBLL;
using Data_Transfer_Object.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepositoryBLL authRepositoryBLL;

        public AuthController(IAuthRepositoryBLL authRepositoryBLL)
        {
            this.authRepositoryBLL = authRepositoryBLL;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            return Ok(await authRepositoryBLL.Delete(email));
        }

        //string? filterOn, string? filterQuery, int? pageNumber, int? pageSize

        [HttpPut("GetAll")]
        public async Task<IActionResult> GetAll(GetAllRequestModel request)
        {
            var data = await authRepositoryBLL.GetAll(request);

            return Ok(data);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var data = await authRepositoryBLL.GetByEmail(email);

            return Ok(data);
        }

    }
}
