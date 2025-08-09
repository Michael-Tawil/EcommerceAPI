using Microsoft.AspNetCore.Identity;

namespace EcommerceAPI.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}
