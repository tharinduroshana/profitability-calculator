namespace ProfitabilityCalculatorMobile.Models
{
    public class ProfitabilityCalculation
    {
        public double pricePerKilometre { get; set; }
        public double pricePerHour  { get; set; }
        public double noOfKilometres  { get; set; }
        public double noOfHours  { get; set; }
        public double income  { get; set; }
        public double totalDistanceBasedCost  { get; set; }
        public double totalTimeBasedCost  { get; set; }
        public double profitability  { get; set; }
    }
}