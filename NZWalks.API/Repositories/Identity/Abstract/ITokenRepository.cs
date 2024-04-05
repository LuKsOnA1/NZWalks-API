using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Identity.Abstract
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
