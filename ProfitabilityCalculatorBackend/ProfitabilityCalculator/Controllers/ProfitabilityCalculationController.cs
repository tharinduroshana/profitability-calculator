using Microsoft.AspNetCore.Mvc;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Models;

namespace ProfitabilityCalculator.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfitabilityCalculationController : ControllerBase
{
    [HttpPost]
    public IActionResult CalculateProfitability(ProfitabilityCalculationRequest request)
    {
        var profitabilityCalculation = ProfitabilityCalculation.InitializeCalculation(request.PricePerKilometre,
            request.PricePerHour, request.NoOfKilometres, request.NoOfHours, request.Income);
        
        // TODO: Implement web service to calculate profitability.

        return Ok();
    }
}