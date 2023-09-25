namespace ProfitabilityCalculator.Contracts;

public record ProfitabilityCalculationRequest(
    double PricePerKilometre,
    double PricePerHour,
    double noOfKilometres,
    double noOfHours,
    double Income
);