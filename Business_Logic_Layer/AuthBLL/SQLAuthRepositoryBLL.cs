using AutoMapper;
using Data_Access_Layer.Services.Auth;
using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.GetAll;
using DemoBadminton.Model;

namespace Business_Logic_Layer.AuthBLL
{
    public class SQLAuthRepositoryBLL : IAuthRepositoryBLL
    {
        private readonly IAuthRepository repository;
        private readonly IMapper mapper;

        public SQLAuthRepositoryBLL(IAuthRepository repository,IMapper _mapper)
        {
            this.repository = repository;
            this.mapper = _mapper;
        }

        //Change password
        public async Task<string> ChangePassword(PasswordRequestModel passwordRequest)
        {
            if(passwordRequest.NewPassword == passwordRequest.ConfirmPassword)
            {
                var result = await repository.ChangePassword(passwordRequest);
                if(result == true)
                {
                    return "Change Password success";
                }
            }
            return "The passwords do not match";
        }

        //Delete
        public async Task<string> Delete(string email)
        {
            var user = await repository.GetByEmail(email);
            if (user != null)
            {
                var result = await repository.Delete(email);
                if (result == true)
                {
                    return "Delete success";
                }
            }
            return "Incorrect is email";
        }

        //Get All
        public async Task<List<UserDTO>> GetAll(GetAllRequestModel request)
        {
            return  mapper.Map<List<UserDTO>>(await repository.GetAll(request));
        }

        //Get By Email
        public async Task<UserDTO?> GetByEmail(string email)
        {
            var result = await repository.GetByEmail(email);
            var roles = await repository.GetRoles(email);
            if(roles != null && result != null)
            {
                result.Roles = roles;
                return result;
            }
            return null;
        }

        //Login
        public async Task<TokenDTO> Login(LoginDTO login)
        {
            return await repository.Login(login);
        }

        //Register
        public async Task<string> Register(RegisterDTO register)
        {
            var user = await repository.GetByEmail(register.Email);

            if (user == null)
            {
                return await repository.Register(register);
            }
            return "Email Used";
        }

        //Reset password
        public async Task<string> ResetPassword(PasswordRequestModel passwordRequest)
        {
            if(passwordRequest.NewPassword == passwordRequest.ConfirmPassword)
            {
                var result = await repository.ResetPassword(passwordRequest);
                if(result == true)
                {
                    return "Reset Password success";
                }
            }
            return "The passwords do not match";
        }

        //Update
        public async Task<string> Update(string email, UserDTO acc)
        {
            var user = await repository.GetByEmail(email);
            if (user != null)
            {
                var result = await repository.Update(email, acc);
                if (result == true)
                {
                    return "Update success";
                }
            }
            return "Incorrect is email";

        }
    }
}
