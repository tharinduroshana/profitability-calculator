using ProfitabilityCalculator.Contracts;

namespace ProfitabilityCalculator.Services.ProfitabilityCalculation;

public class ProfitabilityCalculationService : IProfitabilityCalculationService
{
    public ProfitabilityCalculationResponse CalculateProfitability(
        Models.ProfitabilityCalculation profitabilityCalculation)
    {
        var totalDistanceBasedCosts = CalculateTotalDistanceBasedCosts(profitabilityCalculation.PricePerKilometre, profitabilityCalculation.NoOfKilometres);
        var totalTimeBasedCosts = CalculateTotalTimeBasedCosts(profitabilityCalculation.PricePerHour, profitabilityCalculation.NoOfHours);
        var totalCost = CalculateTotalCosts(totalDistanceBasedCosts, totalTimeBasedCosts);

        var profitability = profitabilityCalculation.Income - totalCost;

        return new ProfitabilityCalculationResponse(profitabilityCalculation.Id,
            profitabilityCalculation.PricePerKilometre, profitabilityCalculation.PricePerHour,
            profitabilityCalculation.NoOfKilometres, profitabilityCalculation.NoOfHours,
            profitabilityCalculation.Income, totalDistanceBasedCosts, totalTimeBasedCosts, profitability);
    }

    public double CalculateTotalDistanceBasedCosts(double pricePerKilometre, double noOfKilometres)
    {
        return pricePerKilometre * noOfKilometres;
    }

    public double CalculateTotalTimeBasedCosts(double pricePerHour, double noOfHours)
    {
        return pricePerHour * noOfHours;
    }

    public double CalculateTotalCosts(double totalDistanceBasedCosts, double totalTimeBasedCosts)
    {
        return totalDistanceBasedCosts + totalTimeBasedCosts;
    }
}