using ProfitabilityCalculator.Models;
using ProfitabilityCalculator.Services.ProfitabilityCalculation;

namespace ProfitabilityCalculator.Tests;

public class ProfitabilityCalculationTests : IDisposable
{
    private readonly ProfitabilityCalculationService _profitabilityCalculationService = new();

    [Theory]
    [InlineData(50, 10, 500)]
    [InlineData(20.5, 5.8, 118.9)]
    [InlineData(-50, 3, -150)]
    [InlineData(65.42, -4, -261.68)]
    [InlineData(-75, -2, 150)]
    public void TestTotalDistanceBasedCosts(double pricePerKilometre, double noOfKilometres, double expected)
    {
        var initProfitabilityCalc = new ProfitabilityCalculation
        {
            PricePerKilometre = pricePerKilometre,
            NoOfKilometres = noOfKilometres
        };

        if (initProfitabilityCalc.PricePerKilometre < 0 || initProfitabilityCalc.NoOfKilometres < 0)
        {
            Assert.Throws<ArgumentException>(() => _profitabilityCalculationService.CalculateTotalDistanceBasedCosts(
                initProfitabilityCalc.PricePerKilometre,
                initProfitabilityCalc.NoOfKilometres));
        }
        else
        {
            var calculatedTotalDistanceBasedCosts = _profitabilityCalculationService.CalculateTotalDistanceBasedCosts(
                initProfitabilityCalc.PricePerKilometre,
                initProfitabilityCalc.NoOfKilometres);

            Assert.Equal(expected, calculatedTotalDistanceBasedCosts);
        }
    }

    [Theory]
    [InlineData(100, 3, 300)]
    [InlineData(120.82, 8, 966.56)]
    [InlineData(-50, 3, -150)]
    [InlineData(65.42, -4, -261.68)]
    [InlineData(-75, -2, 150)]
    public void TestTotalTimeBasedCosts(double pricePerHour, double noOfHours, double expected)
    {
        var initProfitabilityCalc = new ProfitabilityCalculation
        {
            PricePerHour = pricePerHour,
            NoOfHours = noOfHours
        };

        if (initProfitabilityCalc.PricePerHour < 0 || initProfitabilityCalc.NoOfHours < 0)
        {
            Assert.Throws<ArgumentException>(() => _profitabilityCalculationService.CalculateTotalTimeBasedCosts(
                initProfitabilityCalc.PricePerHour,
                initProfitabilityCalc.NoOfHours));
        }
        else
        {
            var calculatedTotalTimeBasedCosts = _profitabilityCalculationService.CalculateTotalTimeBasedCosts(
                initProfitabilityCalc.PricePerHour,
                initProfitabilityCalc.NoOfHours);

            Assert.Equal(expected, calculatedTotalTimeBasedCosts);
        }
    }

    [Theory]
    [InlineData(1000, 3000, 4000)]
    [InlineData(295.58, 422.23, 717.81)]
    [InlineData(-1000, 2000, 3000)]
    [InlineData(523.45, -200.56, 322.89)]
    [InlineData(-225, -400, -625)]
    public void TestTotalCosts(double totalDistanceBasedCosts, double totalTimeBasedCosts, double expected)
    {
        var initProfitabilityCalc = new ProfitabilityCalculation
        {
            TotalDistanceBasedCost = totalDistanceBasedCosts,
            TotalTimeBasedCost = totalTimeBasedCosts
        };

        if (initProfitabilityCalc.TotalDistanceBasedCost < 0 || initProfitabilityCalc.TotalTimeBasedCost < 0)
        {
            Assert.Throws<ArgumentException>(() => _profitabilityCalculationService.CalculateTotalCosts(
                initProfitabilityCalc.TotalDistanceBasedCost,
                initProfitabilityCalc.TotalTimeBasedCost));
        }
        else
        {
            var calculatedTotalCosts = _profitabilityCalculationService.CalculateTotalCosts(
                initProfitabilityCalc.TotalDistanceBasedCost,
                initProfitabilityCalc.TotalTimeBasedCost);

            Assert.Equal(expected, calculatedTotalCosts);
        }
    }

    [Theory]
    [InlineData(50, 4, 100, 3, 700, 200)]
    [InlineData(45.70, 4.5, 95.27, 3, 732.46, 241)]
    [InlineData(-20, 3, -40, 2, 200, 340)]
    public void TestProfitability(double pricePerKilometre, double noOfKilometres, double pricePerHour,
        double noOfHours, double income, double expected)
    {
        var initProfitabilityCalc = new ProfitabilityCalculation
        {
            Income = income,
            PricePerKilometre = pricePerKilometre,
            PricePerHour = pricePerHour,
            NoOfKilometres = noOfKilometres,
            NoOfHours = noOfHours
        };

        if (initProfitabilityCalc.Income < 0 || initProfitabilityCalc.PricePerKilometre < 0 ||
            initProfitabilityCalc.NoOfKilometres < 0 || initProfitabilityCalc.PricePerHour < 0 ||
            initProfitabilityCalc.NoOfHours < 0)
        {
            Assert.Throws<ArgumentException>(() =>
                _profitabilityCalculationService.CalculateProfitability(initProfitabilityCalc).Profitability);
        }
        else
        {
            var calculatedProfitability =
                _profitabilityCalculationService.CalculateProfitability(initProfitabilityCalc).Profitability;

            Assert.Equal(expected, calculatedProfitability);
        }
    }

    public void Dispose()
    {
    }
}