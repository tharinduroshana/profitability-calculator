namespace ProfitabilityCalculator.Models;

public class ProfitabilityCalculation
{
    public Guid Id { get; set; }

    public double PricePerKilometre { get; set; }

    public double PricePerHour { get; set; }

    public double NoOfKilometres { get; set; }

    public double NoOfHours { get; set; }

    public double Income { get; set; }

    public double Profitability { get; set; }

    private ProfitabilityCalculation(double pricePerKilometre, double pricePerHour, double noOfKilometres,
        double noOfHours, double income)
    {
        Id = Guid.NewGuid();
        PricePerKilometre = pricePerKilometre;
        PricePerHour = pricePerHour;
        NoOfKilometres = noOfKilometres;
        NoOfHours = noOfHours;
        Income = income;
    }
    
    public ProfitabilityCalculation() {}

    public static ProfitabilityCalculation InitializeCalculation(double pricePerKilometre, double pricePerHour, double noOfKilometres,
        double noOfHours, double income)
    {
        return new ProfitabilityCalculation(pricePerKilometre, pricePerHour, noOfKilometres, noOfHours, income);
    }
}