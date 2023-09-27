namespace ProfitabilityCalculator.Contracts;

public record UserSignUpRequest(
    string Username,
    string Password,
    string Name
);