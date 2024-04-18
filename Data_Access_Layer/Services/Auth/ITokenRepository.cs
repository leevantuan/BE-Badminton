using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer.Services.Auth
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
