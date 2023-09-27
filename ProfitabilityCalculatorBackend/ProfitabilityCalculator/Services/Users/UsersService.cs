using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
}