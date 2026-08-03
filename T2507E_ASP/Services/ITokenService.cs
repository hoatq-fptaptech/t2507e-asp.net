using T2507E_ASP.Entities;

namespace T2507E_ASP.Services;

public interface ITokenService
{
    (string Token, DateTime ExpireAt) CreateToken(User user);
}