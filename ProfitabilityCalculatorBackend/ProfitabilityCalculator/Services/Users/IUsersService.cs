using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Services.Users;

public interface IUsersService
{
    Task<User> CreateUser(User user);
    Task<User> SignIn(string username);
}