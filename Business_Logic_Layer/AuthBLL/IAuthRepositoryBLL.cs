using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.GetAll;
using DemoBadminton.Model;

namespace Business_Logic_Layer.AuthBLL
{
    public interface IAuthRepositoryBLL
    {
        public Task<string> Register(RegisterDTO register);

        public Task<TokenDTO> Login(LoginDTO login);

        public Task<List<UserDTO>> GetAll(GetAllRequestModel request);

        public Task<UserDTO?> GetByEmail(string email);

        public Task<string> Update(string email, UserDTO acc);

        public Task<string> ResetPassword(PasswordRequestModel passwordRequest);

        public Task<string> ChangePassword(PasswordRequestModel passwordRequest);

        public Task<string> Delete(string email);
    }
}
