using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfitabilityCalculatorMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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