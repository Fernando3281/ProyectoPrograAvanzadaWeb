using Backend.DTO;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services.Interfaces
{
    public interface ITokenService
    {
        TokenModel CreateToken (IdentityUser user, List<string> roles);
    }
}
