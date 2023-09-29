using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Services.Users;

public interface IUsersService
{
    Task<User> CreateUser(UserSignUpRequest request);
    Task<string> SignIn(UserLoginRequest request);
    Task<bool> DeleteUser(UserDeleteRequest request);
}