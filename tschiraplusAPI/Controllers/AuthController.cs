using Microsoft.AspNetCore.Mvc;
using tschiraplusAPI.Models;
using tschiraplusAPI.Services;

namespace tschiraplusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly TokenService _tokenService;

    public AuthController(IUserService userService, TokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
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

    [HttpPost("VerifyToken")]
    public async Task<IActionResult> VerifyToken([FromBody] TokenVerificationRequest request)
    {
        Console.WriteLine("Received token verification request.");
        if (string.IsNullOrEmpty(request.Token))
        {
            return BadRequest(new { Message = "Token is required" });
        }

        try
        {
            var user = await _tokenService.ValidateToken(request.Token);

            if (user == null)
            {
                Console.WriteLine("No user for token found.");
                return Unauthorized(new { Message = "Invalid or expired token" });
            }

            Console.WriteLine("User for token found: " + user.Username);
            return Ok(new
            {
                Message = "Token is valid",
                User = new
                {
                    user.UserId,
                    user.Username,
                    user.Email
                }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("Error during token validation: " + e);
            return StatusCode(500, new { Message = "An error occured during token validation.", Error = e.Message });
        }
    }
}