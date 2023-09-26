namespace ProfitabilityCalculator.Contracts;

public record ProfitabilityCalculationRequest(
    double PricePerKilometre,
    double PricePerHour,
    double NoOfKilometres,
    double NoOfHours,
    double Income
);