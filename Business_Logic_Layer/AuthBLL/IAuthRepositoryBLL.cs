using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.GetAll;
using DemoBadminton.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
