using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Database;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Services.Users;

/*
 * UsersService contains the business logic behind Registering a user and Logging a user in.
 * 
 */
public class UsersService : IUsersService
{
    private readonly ProbabilityCalcDBContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public UsersService(ProbabilityCalcDBContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }
    
    /*
     * CreateUser method registers a user
     *
     * @request: Registering user request
     */
    public async Task<User> CreateUser(UserSignUpRequest request)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Username == request.Username))
        {
            return null;
        }
        else
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Username = request.Username,
                Name = request.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var createdUser = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return createdUser.Entity;
        }
    }

    /*
     * SignIn method logs a user in and returns the jwt token
     *
     * @request: Logging user request
     */
    public async Task<string> SignIn(UserLoginRequest request)
    {
        var fetchedUser = await _dbContext.Users.FindAsync(request.Username);
        if (fetchedUser == null)
        {
            return null;
        }
        var doesPasswordsMatch = VerifyPasswordHash(request.Password, fetchedUser.PasswordHash, fetchedUser.PasswordSalt);
        if (!doesPasswordsMatch)
        {
            return null;
        }
        var token = CreateToken(fetchedUser);
        return token;
    }
    
    /*
     * DeleteUser method deletes an existing user
     *
     * @username: Username of the user which must be deleted
     */
    public async Task<bool> DeleteUser(UserDeleteRequest request)
    {
        var fetchedUser = await _dbContext.Users.FindAsync(request.Username);
        if (fetchedUser == null) return false;
        _dbContext.Remove(fetchedUser);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    /*
     * CreatePasswordHash method creates the hashed password when registering a user
     *
     * @password: The original registering password
     * @passwordHash: The hashed password of registering user
     * @passwordSalt: The password salt of the registering user
     */
    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    
    /*
     * VerifyPasswordHash method does the verification of passwords when logging in
     *
     * @password: The login password
     * @passwordHash: The hashed password of possible user
     * @passwordSalt: The password salt of the possible user
     */
    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
    
    /*
     * CreateToken method creates a jwt token when the authentication succeeds
     *
     * @user: The user which requests for the jwt token
     */
    private string CreateToken(User user)
    {
        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Username),
        });
        
        var issuer = _configuration.GetSection("AppSettings:Issuer").Value;
        var audience = _configuration.GetSection("AppSettings:Audience").Value;
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
            SecurityAlgorithms.HmacSha512Signature
        );
        
        var expires = DateTime.Now.AddDays(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}