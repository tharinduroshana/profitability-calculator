using ProfitabilityCalculator.Contracts;

namespace ProfitabilityCalculator.Services.ProfitabilityCalculation;

public interface IProfitabilityCalculationService
{
    ProfitabilityCalculationResponse CalculateProfitability(Models.ProfitabilityCalculation profitabilityCalculation);
    double CalculateTotalDistanceBasedCosts(double pricePerKilometre, double noOfKilometres);
    double CalculateTotalTimeBasedCosts(double pricePerHour, double noOfHours);
    double CalculateTotalCosts(double totalDistanceBasedCosts, double totalTimeBasedCosts);
}