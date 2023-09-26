using ProfitabilityCalculator.Contracts;

namespace ProfitabilityCalculator.Services.ProfitabilityCalculation;

public class ProfitabilityCalculationService : IProfitabilityCalculationService
{
    public ProfitabilityCalculationResponse CalculateProfitability(
        Models.ProfitabilityCalculation profitabilityCalculation)
    {
        var costPerKilometres = profitabilityCalculation.PricePerKilometre * profitabilityCalculation.NoOfKilometres;
        var costPerHours = profitabilityCalculation.PricePerHour * profitabilityCalculation.NoOfHours;
        var totalCost = costPerKilometres + costPerHours;

        var profitability = profitabilityCalculation.Income - totalCost;

        return new ProfitabilityCalculationResponse(profitabilityCalculation.Id,
            profitabilityCalculation.PricePerKilometre, profitabilityCalculation.PricePerHour,
            profitabilityCalculation.NoOfKilometres, profitabilityCalculation.NoOfHours,
            profitabilityCalculation.Income, costPerKilometres, costPerHours, profitability);
    }
}