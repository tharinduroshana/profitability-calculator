using System;
using System.Net;
using System.Threading.Tasks;
using ProfitabilityCalculatorMobile.Models;
using ProfitabilityCalculatorMobile.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This screen represents the profitability calculator for mobile
 * 
 * Takes user inputs, validates and sends the request to calculate profitability in the backend.
 * Shows AlertDialogs in case an issue occurs.
 */
namespace ProfitabilityCalculatorMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PricePerKilometre.Focus();
        }

        private async void Calculate(object sender, EventArgs e)
        {
            var costPerKilometre = PricePerKilometre.Text;
            var costPerHour = PricePerHour.Text;
            var kilometres = NoOfKilometres.Text;
            var hours = NoOfHours.Text;
            var income = Income.Text;

            var isValid = await ValidateInputs(costPerKilometre, costPerHour, kilometres, hours, income);

            try
            {
                if (isValid)
                {
                    var profitabilityCalculationReq = new ProfitabilityCalculation
                    {
                        pricePerKilometre = double.Parse(costPerKilometre),
                        pricePerHour = double.Parse(costPerHour),
                        noOfKilometres = double.Parse(kilometres),
                        noOfHours = double.Parse(hours),
                        income = double.Parse(income),
                    };

                    var response = await RequestUtils.SendPostRequest(
                        Constants.ApiBaseUrl + "/profitabilityCalculation",
                        profitabilityCalculationReq, true);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();

                        Preferences.Set("calculatedResults", jsonString);

                        await Navigation.PushAsync(new ResultsPage());
                        Navigation.RemovePage(this);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DisplayAlert("Error", "An error has occured while processing the request!", "Ok");
            }
        }

        private async Task<bool> ValidateInputs(string costPerKilometre, string costPerHour, string kilometres,
            string hours,
            string income)
        {
            if (string.IsNullOrEmpty(costPerKilometre) && string.IsNullOrEmpty(costPerHour) &&
                string.IsNullOrEmpty(kilometres) && string.IsNullOrEmpty(hours) && string.IsNullOrEmpty(income))
            {
                await DisplayAlert("Input Error", "Inputs cannot be left empty", "Ok");
                return false;
            }

            if (double.TryParse(costPerKilometre, out double costPerKilometreDouble) &&
                double.TryParse(costPerHour, out double costPerHourDouble) &&
                double.TryParse(kilometres, out double kilometresDouble) &&
                double.TryParse(hours, out double hoursDouble) && double.TryParse(income, out double incomeDouble))
            {
                if (costPerKilometreDouble < 0 || costPerHourDouble < 0 || kilometresDouble < 0 || hoursDouble < 0 || incomeDouble < 0)
                {
                    await DisplayAlert("Input Error", "Fields contain invalid numbers", "Ok");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                await DisplayAlert("Input Error", "Fields contain invalid numbers", "Ok");
                return false;
            }
        }

        private async void LogOut(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}