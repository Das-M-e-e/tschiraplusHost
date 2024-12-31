using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using tschiraplusAPI.Models;
using tschiraplusAPI.Repositories;

namespace tschiraplusAPI.Services;

public class UserService : IUserService
{
    private readonly PasswordHasher<UserModel> _passwordHasher = new();
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public UserService(IUserRepository userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ServiceResponse> RegisterUserAsync(RegisterUserDto registerDto)
    {
        if (await _userRepository.UsernameExists(registerDto.Username))
        {
            return new ServiceResponse
            {
                Success = false,
                Message = "Username already exists"
            };
        }

        var hashedPassword = _passwordHasher.HashPassword(null, registerDto.Password);

        var newUser = new UserModel
        {
            UserId = Guid.NewGuid(),
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.CreateUserAsync(newUser);

        var token = _tokenService.GenerateToken(newUser.UserId, newUser.Username, new[] { "User" });

        return new ServiceResponse
        {
            Success = true,
            Data = token
        };
    }

    public async Task<ServiceResponse> AuthenticateUserAsync(LoginUserDto loginDto)
    {
        var user = await _userRepository.GetUserByIdentifierAsync(loginDto.Identifier);
        if (user == null)
        {
            Console.WriteLine("User not found in AuthenticateUserAsync()");
            return new ServiceResponse
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        Console.WriteLine(result);
        if (result != PasswordVerificationResult.Success)
        {
            Console.WriteLine("Password verification was not successful");
            return new ServiceResponse
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        var token = _tokenService.GenerateToken(user.UserId, user.Username, new[] { "User" });

        return new ServiceResponse
        {
            Success = true,
            Data = token
        };
    }

    public async Task<ServiceResponse> GetUserByIdentifierAsync(string identifier)
    {
        var user = await _userRepository.GetUserByIdentifierAsync(identifier);
        if (user == null)
        {
            Console.WriteLine("User not found");
            return new ServiceResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        Console.WriteLine("User found: " + user.Username);
        return new ServiceResponse
        {
            Success = true,
            Data = user
        };
    }
}