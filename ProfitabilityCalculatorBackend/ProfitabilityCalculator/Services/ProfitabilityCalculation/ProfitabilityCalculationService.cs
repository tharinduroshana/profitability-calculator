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

        var profitability = Math.Round(profitabilityCalculation.Income - totalCost, 2);

        return new ProfitabilityCalculationResponse(profitabilityCalculation.Id,
            profitabilityCalculation.PricePerKilometre, profitabilityCalculation.PricePerHour,
            profitabilityCalculation.NoOfKilometres, profitabilityCalculation.NoOfHours,
            profitabilityCalculation.Income, totalDistanceBasedCosts, totalTimeBasedCosts, profitability);
    }

    public double CalculateTotalDistanceBasedCosts(double pricePerKilometre, double noOfKilometres)
    {
        if (pricePerKilometre < 0 || noOfKilometres < 0)
        {
            throw new ArgumentException("Found unsupported arguments while calculating total distance based costs!");
        }
        
        return Math.Round(pricePerKilometre * noOfKilometres, 2);
    }

    public double CalculateTotalTimeBasedCosts(double pricePerHour, double noOfHours)
    {
        if (pricePerHour < 0 || noOfHours < 0)
        {
            throw new ArgumentException("Found unsupported arguments while calculating total time based costs!");
        }
        
        return Math.Round(pricePerHour * noOfHours, 2);
    }

    public double CalculateTotalCosts(double totalDistanceBasedCosts, double totalTimeBasedCosts)
    {
        if (totalDistanceBasedCosts < 0 || totalTimeBasedCosts < 0)
        {
            throw new ArgumentException("Found unsupported arguments while calculating total costs!");
        }
        
        return Math.Round(totalDistanceBasedCosts + totalTimeBasedCosts, 2);
    }
}