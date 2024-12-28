using Microsoft.AspNetCore.Mvc;
using tschiraplusAPI.Models;
using tschiraplusAPI.Repositories;

namespace tschiraplusAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    // GET: api/Users/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserModel>> GetUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    
    // POST: api/Users
    [HttpPost]
    public async Task<ActionResult<UserModel>> PostUser(UserModel userModel)
    {
        var userExists = await _userRepository.UserExistsAsync(userModel.UserId, userModel.Email);
        if (userExists)
        {
            return BadRequest("Email already exists!");
        }

        await _userRepository.CreateUserAsync(userModel);
        return CreatedAtAction(nameof(GetUser), new { id = userModel.UserId }, userModel);
    }
    
    // PUT: api/Users/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutUser(Guid id, UserModel userModel)
    {
        if (id != userModel.UserId)
        {
            return BadRequest("User ID mismatch.");
        }
        
        try
        {
            await _userRepository.UpdateUserAsync(userModel);
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found.");
        }

        return NoContent();
    }
    
    // DELETE: api/Users/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userRepository.DeleteUserAsync(id);
        return NoContent();
    }
}