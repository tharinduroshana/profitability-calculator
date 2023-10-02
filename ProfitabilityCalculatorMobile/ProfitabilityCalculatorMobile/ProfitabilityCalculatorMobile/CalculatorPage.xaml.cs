using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            await Navigation.PushAsync(new ResultsPage());
            Navigation.RemovePage(this);
        }
    }
}