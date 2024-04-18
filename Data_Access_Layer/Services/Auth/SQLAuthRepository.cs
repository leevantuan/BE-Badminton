using AutoMapper;
using Data_Transfer_Object.AuthDTO;
using Data_Transfer_Object.GetAll;
using DemoBadminton.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Data_Access_Layer.Services.Auth
{
    public class SQLAuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository token;
        private readonly IMapper mapper;

        public SQLAuthRepository(UserManager<IdentityUser> userManager, ITokenRepository token, IMapper mapper)
        {
            this.userManager = userManager;
            this.token = token;
            this.mapper = mapper;
        }

        //Delete
        public async Task<bool> Delete(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return false;
                }

                var deleteResult = await userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get all
        public async Task<List<IdentityUser>> GetAll(GetAllRequestModel request)
        {
            try
            {
                var allUser = userManager.Users.AsQueryable();

                if (string.IsNullOrWhiteSpace(request.FilterOn) == false && string.IsNullOrWhiteSpace(request.FilterQuery) == false)
                {
                    if (request.FilterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    {
                        //Contains lọc phân biệt chữ hoa chữ thường.
                        allUser = allUser.Where(x => x.Email.Contains(request.FilterQuery));
                    }
                }

                if (string.IsNullOrWhiteSpace(request.SortBy) == false)
                {
                    if (request.SortBy.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    {
                        allUser = request.IsAcsending ? allUser.OrderBy(x => x.Email) : allUser.OrderByDescending(x => x.Email);
                    }
                }

                var skipResult = (request.PageNumber - 1) * request.PageSize;

                var list = await allUser.Skip(skipResult).Take(request.PageSize).ToListAsync();


                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get By Email
        public async Task<UserDTO?> GetByEmail(string? email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return null;
                }

                var results = mapper.Map<UserDTO>(user);

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Login
        public async Task<TokenDTO> Login(LoginDTO login)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var checkPasswordResult = await userManager.CheckPasswordAsync(user, login.Password);

                    if (checkPasswordResult)
                    {
                        var role = await userManager.GetRolesAsync(user);

                        if (role != null)
                        {
                            var response = GenerateToken(user, role.ToList());
                            return response;
                        }

                        return new TokenDTO
                        {
                            Message = "Success"
                        };
                    }
                }

                return new TokenDTO
                {
                    Message = "UserName or Password iconrrect"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Register
        public async Task<string> Register(RegisterDTO register)
        {
            try
            {
                var identityUser = new IdentityUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                };

                var identityResult = await userManager.CreateAsync(identityUser, register.Password);

                if (identityResult.Succeeded)
                {
                    if (register.Roles != null && register.Roles.Any())
                    {
                        identityResult = await userManager.AddToRolesAsync(identityUser, register.Roles);

                        if (identityResult.Succeeded)
                        {
                            return "User was registed! Login please!";
                        }
                    }
                }
                return "Something went wrong!";
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Reset Password
        public async Task<bool> ResetPassword(PasswordRequestModel passwordRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(passwordRequest.email);

                if (user == null)
                {
                    return false;
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var resetPassword = await userManager.ResetPasswordAsync(user, token, passwordRequest.NewPassword);

                if (resetPassword.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Change Password
        public async Task<bool> ChangePassword(PasswordRequestModel passwordRequest)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(passwordRequest.email);

                if (user == null)
                {
                    return false;
                }

                var changePassword = await userManager.ChangePasswordAsync(user, passwordRequest.PrevPassword, passwordRequest.NewPassword);

                if (changePassword.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Update
        public async Task<bool> Update(string email, UserDTO acc)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return false;
                }

                user.PhoneNumber = acc.PhoneNumber;
                user.UserName = acc.UserName;
                user.Email = acc.Email;

                var updateResult = await userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Create token
        private TokenDTO GenerateToken([FromBody] IdentityUser user, List<string>? role)
        {
            //create Token
            var jwtToken = token.CreateJwtToken(user, role.ToList());

            return new TokenDTO
            {
                Message = "Success",
                AccessToken = jwtToken,
                RefreshToken = GenerateRefreshToken()
            };
        }

        //RefreshToken
        private string GenerateRefreshToken()
        {
            var ramdom = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(ramdom);

                return Convert.ToBase64String(ramdom);
            }
        }

        //Get Roles
        public async Task<List<string>?> GetRoles(string email)
        {
            try
            {
                var result = new List<string>();

                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return result;
                }

                var roles = await userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    result.Add(role.ToString());
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
