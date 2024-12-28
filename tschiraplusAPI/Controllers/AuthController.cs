using Microsoft.AspNetCore.Mvc;
using tschiraplusAPI.Models;
using tschiraplusAPI.Services;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        var response = await _userService.RegisterUserAsync(registerUserDto);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(new { Token = response.Data });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var response = await _userService.AuthenticateUserAsync(loginUserDto);
        if (!response.Success)
        {
            return Unauthorized("Invalid credentials");
        }

        var userResponse = await _userService.GetUserByIdentifierAsync(loginUserDto.Identifier);
        if (!userResponse.Success || userResponse.Data == null)
        {
            return NotFound("User not found");
        }

        var user = (UserModel)userResponse.Data;

        return Ok(new
        {
            Token = response.Data,
            User = new
            {
                user.UserId,
                user.Username,
                Roles = new[] { "User" }
            }
        });
    }
}