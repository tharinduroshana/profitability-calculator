using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Calculates the Profitability of provided Quotation
    /// </summary>
    /// <remarks>
    /// Use this endpoint to calculate total distance based costs, total time based costs and profitability.
    /// </remarks>
    /// <param name="request">The request object contains totalCostPerKilometre, totalCostPerHour, noOfHours, noOfKilometres, Income</param>
    /// <returns>The response contains the same information provided above with the id, total distance based costs, total time based costs and profitability</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public ActionResult<ProfitabilityCalculationResponse> CalculateProfitability(ProfitabilityCalculationRequest request)
    {
        try
        {
            var profitabilityCalculation = ProfitabilityCalculation.InitializeCalculation(request.PricePerKilometre,
                request.PricePerHour, request.NoOfKilometres, request.NoOfHours, request.Income);

            var response = _profitabilityCalculationService.CalculateProfitability(profitabilityCalculation);

            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Calculation failed due to invalid arguments!");
        }
    }
}