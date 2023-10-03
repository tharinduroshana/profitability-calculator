using System;
using Newtonsoft.Json;
using ProfitabilityCalculatorMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This screen shows profitability calculations to the user
 *
 * This page takes saved calculation results from the Shared Preferences and display to the user.
 */
namespace ProfitabilityCalculatorMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        private async void BackToCalculatorPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalculatorPage());
            Navigation.RemovePage(this);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var calculatedResults = Preferences.Get("calculatedResults", null);
            if (!string.IsNullOrEmpty(calculatedResults))
            {
                var calculationObj = JsonConvert.DeserializeObject<ProfitabilityCalculation>(calculatedResults);
                DistanceBasedCosts.Text = calculationObj.totalDistanceBasedCost.ToString();
                TimeBasedCosts.Text = calculationObj.totalTimeBasedCost.ToString();
                Income.Text = calculationObj.income.ToString();
                Profitability.Text = calculationObj.profitability.ToString();
            }
        }
    }
}