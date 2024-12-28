using Microsoft.EntityFrameworkCore;
using tschiraplusAPI.Data;
using tschiraplusAPI.Models;

namespace tschiraplusAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel?> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<UserModel?> GetUserByIdentifierAsync(string identifier)
    {
        if (identifier.Contains("@"))
        {
            Console.WriteLine("User login via email");
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == identifier);
        }

        Console.WriteLine("User login via username");
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == identifier);
    }

    public async Task<bool> UserExistsAsync(Guid userId, string email)
    {
        return await _context.Users.AnyAsync(u => u.UserId == userId || u.Email == email);
    }

    public async Task<bool> UsernameExists(string username)
    {
        return false;
    }

    public async Task CreateUserAsync(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserModel userModel)
    {
        var existingUser = await GetUserByIdAsync(userModel.UserId);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        existingUser = userModel;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}