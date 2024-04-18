using Business_Logic_Layer.AuthBLL;
using Data_Transfer_Object.AuthDTO;
using DemoBadminton.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthRepositoryBLL authRepositoryBLL;

        public UserController(IAuthRepositoryBLL authRepositoryBLL)
        {
            this.authRepositoryBLL = authRepositoryBLL;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            return Ok(await authRepositoryBLL.Register(register));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            return Ok(await authRepositoryBLL.Login(login));
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordRequestModel passwordRequest)
        {
            return Ok(await authRepositoryBLL.ResetPassword(passwordRequest));
        }

        [HttpPost]
        //[Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordRequestModel passwordRequest)
        {
            return Ok(await authRepositoryBLL.ChangePassword(passwordRequest));
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> Update(string email, [FromBody] UserDTO user)
        {
            return Ok(await authRepositoryBLL.Update(email, user));
        }

    }
}
