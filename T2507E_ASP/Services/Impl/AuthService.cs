using Microsoft.AspNetCore.Identity;
using T2507E_ASP.DTOs.Auth;
using T2507E_ASP.Entities;
using T2507E_ASP.Models;
using T2507E_ASP.Repositories;

namespace T2507E_ASP.Services.Impl;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, ITokenService tokenService, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<ApiResult<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _userRepository.FindByEmailAsync(
            request.Email.Trim().ToLowerInvariant());
        if (userExists != null)
        {
            return ApiResult<AuthResponse>.Failure("EMAIL_ALREADY_EXISTS"
                ,"Email already exists");
        }

        var user = new User
        {
            Email = request.Email.Trim().ToLowerInvariant(),
            FullName = request.FullName,
            Role = "User",
            IsActive = true,
            CreateAt = DateTime.UtcNow
        };
        user.Password = _passwordHasher.HashPassword(user, request.Password);
        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangeAsync();
        _logger.LogInformation("User created successfully with email: {Email}",user.Email);
        var (token,expiresAt) = _tokenService.CreateToken(user);
        var authResponse = new AuthResponse
        {
            Id =  user.Id,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName,
            AccessToken = token,
            ExpiresAt = expiresAt
        };
        return ApiResult<AuthResponse>.Success(authResponse);
    }

    public async Task<ApiResult<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(
            request.Email.Trim().ToLowerInvariant());
        if (user is null)
        {
            return ApiResult<AuthResponse>.Failure("EMAIL_OR_PASSWORD_INVALID");
        }

        if (!user.IsActive)
        {
            return ApiResult<AuthResponse>.Failure("EMAIL_NOT_ACTIVE");
        }

        var passwordVerify = _passwordHasher.VerifyHashedPassword(
                    user, user.Password, request.Password);
        if (passwordVerify == PasswordVerificationResult.Failed)
        {
            return ApiResult<AuthResponse>.Failure("EMAIL_OR_PASSWORD_INVALID");
        }
        var (token,expiresAt) = _tokenService.CreateToken(user);
        var authResponse = new AuthResponse
        {
            Id =  user.Id,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName,
            AccessToken = token,
            ExpiresAt = expiresAt
        };
        return ApiResult<AuthResponse>.Success(authResponse);
    }
}