namespace ProfitabilityCalculator.Contracts;

public record ProfitabilityCalculationResponse(
    Guid Id,
    double PricePerKilometre,
    double PricePerHour,
    double noOfKilometres,
    double noOfHours,
    double Income
);