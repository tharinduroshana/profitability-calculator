using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Models;
using ProfitabilityCalculator.Services.Users;

namespace ProfitabilityCalculator.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersService _userService;

    public UserController(IConfiguration configuration, IUsersService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<User>> SignUp(UserSignUpRequest request)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            Username = request.Username,
            Name = request.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var createdUser = await _userService.CreateUser(user);

        var response = new UserResponse(createdUser.Name, createdUser.Username);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginRequest request)
    {
        var user = await _userService.SignIn(request.Username);

        if (user == null)
        {
            return BadRequest("User not found!");
        }

        var doesPasswordsMatch = VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

        if (!doesPasswordsMatch)
        {
            return BadRequest("Wrong credentials!");
        }

        var token = CreateToken(user);
        return Ok(token);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Name, user.Name)
        };
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}