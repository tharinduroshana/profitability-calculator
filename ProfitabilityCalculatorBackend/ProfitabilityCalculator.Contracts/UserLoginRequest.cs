namespace ProfitabilityCalculator.Contracts;

public record UserLoginRequest(
    string Username,
    string Password
);