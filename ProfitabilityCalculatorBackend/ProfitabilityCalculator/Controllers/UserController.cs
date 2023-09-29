using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Models;
using ProfitabilityCalculator.Services.Users;

namespace ProfitabilityCalculator.Controllers;

/*
 * UserController contains the endpoints for User Sign Up and Login
 * 
 */
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUsersService _userService;

    public UserController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<User>> SignUp(UserSignUpRequest request)
    {

        var createdUser = await _userService.CreateUser(request);
        if (createdUser == null)
        {
            return BadRequest("User Creation Failed!");
        }
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginRequest request)
    {
        var token = await _userService.SignIn(request);
        if (token == null)
        {
            return BadRequest("Authentication Failed!");
        }

        var response = new UserLoginResponse(request.Username, token);
        return Ok(response);
    }
    
    [HttpDelete("delete")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteUser(UserDeleteRequest request)
    {
        var isDeleted = await _userService.DeleteUser(request);
        if (isDeleted)
        {
            return Ok();
        }
        return BadRequest("User deletion failed!");
    }
}