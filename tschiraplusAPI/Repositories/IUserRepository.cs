using tschiraplusAPI.Models;

namespace tschiraplusAPI.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel?> GetUserByIdAsync(Guid userId);
    Task<UserModel?> GetUserByIdentifierAsync(string identifier);
    Task<bool> UserExistsAsync(Guid userId, string email);
    Task<bool> UsernameExists(string username);
    Task CreateUserAsync(UserModel user);
    Task UpdateUserAsync(UserModel userModel);
    Task DeleteUserAsync(Guid userId);
}