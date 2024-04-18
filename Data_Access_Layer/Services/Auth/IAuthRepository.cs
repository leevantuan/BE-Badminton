using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.GetAll;
using DemoBadminton.Model;
using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer.Services.Auth
{
    public interface IAuthRepository
    {
        public Task<string> Register(RegisterDTO register);

        public Task<TokenDTO> Login(LoginDTO login);

        public Task<List<IdentityUser>> GetAll(GetAllRequestModel request);

        public Task<UserDTO?> GetByEmail(string? email);

        public Task<bool> Update(string email, UserDTO acc);

        public Task<bool> ResetPassword(PasswordRequestModel passwordRequest);

        public Task<bool> ChangePassword(PasswordRequestModel passwordRequest);

        public Task<bool> Delete(string email);

        public Task<List<string>?> GetRoles(string email);

    }
}
