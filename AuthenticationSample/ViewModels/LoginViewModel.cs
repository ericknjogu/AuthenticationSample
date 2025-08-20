
using AuthenticationSample.Services;
using AuthenticationSample.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationSample.ViewModels
{
    
    public partial class LoginViewModel: ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        public LoginViewModel()
        {
            _authService = new AuthService();
         
        }

        [RelayCommand]
        private async void Login()
        {
            

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                return;
            }

            var Success = await _authService.LoginAsync(Username, Password);

            if (Success)
            {

                //await App.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}
