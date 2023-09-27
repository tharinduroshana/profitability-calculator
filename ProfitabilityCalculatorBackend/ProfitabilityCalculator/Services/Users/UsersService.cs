using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfitabilityCalculator.Database;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Services.Users;

public class UsersService : IUsersService
{
    private readonly ProbabilityCalcDBContext _dbContext;
    
    public UsersService(ProbabilityCalcDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> CreateUser(User user)
    {
        var createdUser = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return createdUser.Entity;
    }

    public async Task<User> SignIn(string username)
    {
        var fetchedUser = await _dbContext.Users.FindAsync(username);
        return fetchedUser!;
    }
}