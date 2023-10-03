using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProfitabilityCalculatorMobile.Models;
using ProfitabilityCalculatorMobile.Utils;
using Xamarin.Essentials;
using Xamarin.Forms;

/*
 * This screen represents the User login
 *
 * Takes user inputs, validates and sends the request to log the user into the application.
 * Shows AlertDialogs in case an issue occurs.
 */
namespace ProfitabilityCalculatorMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
            Navigation.RemovePage(this);
        }

        private async void OnLoginPressed(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            var isValid = ValidateInputs(username, password);

            if (isValid)
            {
                try
                {
                    var user = new User
                    {
                        username = username,
                        password = password
                    };

                    var response = await RequestUtils.SendPostRequest(Constants.ApiBaseUrl + "/user/login", user);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var signInResponse = JsonConvert.DeserializeObject<SignInResponse>(content);
                        StoreToken(signInResponse.username, signInResponse.token);
                        await Navigation.PushAsync(new CalculatorPage());
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        await DisplayAlert("Error", "Authentication failed!", "Ok");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    await DisplayAlert("Error", "Authentication failed!", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Input Error", "Inputs cannot be left empty!", "Ok");
            }
        }

        private async void StoreToken(string username, string token)
        {
            try
            {
                await SecureStorage.SetAsync("username", username);
                await SecureStorage.SetAsync("token", token);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        private bool ValidateInputs(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }
    }
}