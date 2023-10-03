using System;
using ProfitabilityCalculatorMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace ProfitabilityCalculatorMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            CheckForToken();
        }

        private async void CheckForToken()
        {
            try
            {
                var username = await SecureStorage.GetAsync("username");
                var token = await SecureStorage.GetAsync("token");

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(token))
                {
                    MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    MainPage = new NavigationPage(new CalculatorPage());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}