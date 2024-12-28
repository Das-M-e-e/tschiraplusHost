using tschiraplusAPI.Models;

namespace tschiraplusAPI.Services;

public interface IUserService
{
    Task<ServiceResponse> RegisterUserAsync(RegisterUserDto registerDto);
    Task<ServiceResponse> AuthenticateUserAsync(LoginUserDto loginDto);
    Task<ServiceResponse> GetUserByIdentifierAsync(string identifier);
}