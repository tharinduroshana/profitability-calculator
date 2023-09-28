namespace ProfitabilityCalculator.Contracts;

public record UserLoginResponse(
    string Username,
    string Token
);