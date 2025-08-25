
using AuthenticationSample.Services;
using AuthenticationSample.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationSample.ViewModels
{

    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]

        private bool isRunning = false;

        public LoginViewModel()
        {
            _authService = new AuthService();

        }

        [RelayCommand]
        private async void Login()
        {

            try
            {
                IsRunning = true;
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                    return;
                }

                var Success = await _authService.LoginAsync(Username, Password);

                if (Success)
                {

                    //await App.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    Password = string.Empty;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error", "An Error Occured" + ex, "OK");
                Password = string.Empty;
                IsRunning = false;
            }
            finally
            {
                IsRunning = false;
            }


        }

        [RelayCommand]
        private async void ForgotPassword()
        {

            await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
        }
    }
}