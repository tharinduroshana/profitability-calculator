using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfitabilityCalculatorMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UsernameEntry.Focus();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
            Navigation.RemovePage(this);
        }

        private async void OnLoginPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CalculatorPage());
            Navigation.RemovePage(this);
        }
    }
}