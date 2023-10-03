using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProfitabilityCalculatorMobile.Models;
using ProfitabilityCalculatorMobile.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This screen represents the Sign Up page
 *
 * Takes user inputs, validates and sends the request to create a user account.
 * Shows AlertDialogs in case an issue occurs.
 */
namespace ProfitabilityCalculatorMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void BackToLoginButtonPressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }

        private async Task<HttpStatusCode> SignUpUser(User user)
        {
            var response = await RequestUtils.SendPostRequest(Constants.ApiBaseUrl + "/user/signup", user);
            
            return response.StatusCode;
        }

        private async Task<bool> ValidateInputs(string name, string username, string password, string rePassword)
        {
            if (StringNotEmpty(name) || StringNotEmpty(username) || StringNotEmpty(password) || StringNotEmpty(rePassword))
            {
                await DisplayAlert("Input Error", "Inputs cannot be left empty!", "Ok");
                return false;
            }

            if (!IsValidUsername(username))
            {
                await DisplayAlert("Input Error", "Username should be at least 4 characters long!", "Ok");
                return false;
            }

            if (!IsValidPassword(password))
            {
                await DisplayAlert("Input Error", "Password should be at least 8 characters long. At least one capital letter and a number should be included", "Ok");
                return false;
            }

            if (password.Equals(rePassword)) return true;
            
            await DisplayAlert("Input Error", "The two passwords do not match!", "Ok");
            return false;

        }

        private bool IsValidUsername(string username)
        {
            return Regex.Match(username, "^(?=.*[a-zA-Z])[a-zA-Z0-9]{4,}$").Success;
        }
        
        private bool IsValidPassword(string password)
        {
            return Regex.Match(password, "^(?=.*[A-Z])(?=.*[0-9]).{8,}$").Success;
        }

        private bool StringNotEmpty(string input)
        {
            return string.IsNullOrEmpty(input);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FullNameEntry.Focus();
        }

        private async void SignUpUser(object sender, EventArgs e)
        {
            var name = FullNameEntry.Text;
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            var rePassword = RePasswordEntry.Text;

            var validInputs = await ValidateInputs(name, username, password, rePassword);

            try
            {
                if (validInputs)
                {
                    var newUser = new User
                    {
                        name = name,
                        username = username,
                        password = password
                    };

                    var statusCode = await SignUpUser(newUser);

                    if (statusCode == HttpStatusCode.Created)
                    {
                        await DisplayAlert("Success", "The user has been signed up successfully", "Sign In");
                        await Navigation.PushAsync(new MainPage());
                        Navigation.RemovePage(this);
                    }
                    else
                    {
                        await DisplayAlert("Error", "An error has occured while processing the request!", "Ok");
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DisplayAlert("Error", "An error has occured!", "Ok");
            }
        }
    }
}