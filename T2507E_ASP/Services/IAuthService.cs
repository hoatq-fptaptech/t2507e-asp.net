using T2507E_ASP.DTOs.Auth;
using T2507E_ASP.Models;

namespace T2507E_ASP.Services;

public interface IAuthService
{
    Task<ApiResult<AuthResponse>> RegisterAsync(RegisterRequest request);
    Task<ApiResult<AuthResponse>> LoginAsync(LoginRequest request);
}