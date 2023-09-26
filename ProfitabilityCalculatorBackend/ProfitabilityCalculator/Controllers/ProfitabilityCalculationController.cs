using Microsoft.AspNetCore.Mvc;
using ProfitabilityCalculator.Contracts;
using ProfitabilityCalculator.Models;
using ProfitabilityCalculator.Services.ProfitabilityCalculation;

namespace ProfitabilityCalculator.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfitabilityCalculationController : ControllerBase
{
    private readonly IProfitabilityCalculationService _profitabilityCalculationService;

    public ProfitabilityCalculationController(IProfitabilityCalculationService profitabilityCalculationService)
    {
        _profitabilityCalculationService = profitabilityCalculationService;
    }

    [HttpPost]
    public IActionResult CalculateProfitability(ProfitabilityCalculationRequest request)
    {
        var profitabilityCalculation = ProfitabilityCalculation.InitializeCalculation(request.PricePerKilometre,
            request.PricePerHour, request.NoOfKilometres, request.NoOfHours, request.Income);

        var response = _profitabilityCalculationService.CalculateProfitability(profitabilityCalculation);

        return Ok(response);
    }
}