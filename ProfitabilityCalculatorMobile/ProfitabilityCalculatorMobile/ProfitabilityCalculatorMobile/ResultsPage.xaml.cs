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
    }
}