using System.Text.RegularExpressions;
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

    /// <summary>
    /// Registers a new user into the system
    /// </summary>
    /// <remarks>
    /// Use this endpoint to Register a new user which can be used to authenticate a profitability calculation
    /// </remarks>
    /// <param name="request">The request object contains name, username, password of the registering user</param>
    /// <returns>UserSignUpResponse with username and name of the registered user</returns>
    /// <response code="201">Returns when the user is successfully registered.</response>
    /// <response code="400">Returns when the an error occured during registration.</response>
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<UserSignUpResponse>> SignUp(UserSignUpRequest request)
    {
        try
        {
            var isUsernameValid = IsValidUsername(request.Username);
            var isPasswordValid = IsValidPassword(request.Password);

            if (isUsernameValid && isPasswordValid)
            {
                var createdUser = await _userService.CreateUser(request);
                if (createdUser == null)
                {
                    return BadRequest("User Creation Failed!");
                }
                return Created("users/", new UserSignUpResponse(createdUser.Name, createdUser.Username));
            }
            else
            {
                return BadRequest("Provided arguments doesn't meet the requirements!");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error!");
        }
    }

    /// <summary>
    /// Logs a user into the calculator program
    /// </summary>
    /// <remarks>
    /// Use this endpoint to login a user into the system to gain a JWT token to authenticate a profitability calculation
    /// </remarks>
    /// <param name="request">The request object contains username, password of the user</param>
    /// <returns>UserLoginResponse with username and the JWT token</returns>
    /// <response code="200">Returns when the user authenticated successfully.</response>
    /// <response code="400">Returns when the an error occured during login.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest request)
    {
        try
        {
            var token = await _userService.SignIn(request);
            if (token == null)
            {
                return BadRequest("Authentication Failed!");
            }

            var response = new UserLoginResponse(request.Username, token);
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error!");
        }
    }
    
    /// <summary>
    /// Removes a user from the database
    /// </summary>
    /// <remarks>
    /// Use this endpoint to remove a user from the system's database
    /// </remarks>
    /// <param name="request">The request object contains username of the user</param>
    /// <returns>OK Response if succeeds</returns>
    /// <response code="200">Returns when the user removed successfully.</response>
    /// <response code="400">Returns when the an error occured during user removal.</response>
    [HttpDelete("delete")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> DeleteUser(UserDeleteRequest request)
    {
        try
        {
            var isDeleted = await _userService.DeleteUser(request);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest("User deletion failed!");
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal Server Error!");
        }
    }

    private bool IsValidUsername(string username)
    {
        return Regex.Match(username, "^(?=.*[a-zA-Z])[a-zA-Z0-9]{4,}$").Success;
    }
    
    private bool IsValidPassword(string password)
    {
        return Regex.Match(password, "^(?=.*[A-Z])(?=.*[0-9]).{8,}$").Success;
    }
}