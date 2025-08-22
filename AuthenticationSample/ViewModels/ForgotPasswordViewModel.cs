using AuthenticationSample.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace AuthenticationSample.ViewModels
{
    public partial class ForgotPasswordViewModel:ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string newPassword;

        public ForgotPasswordViewModel()
        {
            _authService = new AuthService();
        }

        [RelayCommand]

        public async void UpdatePassword()
        {
            try
            {
                if(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(NewPassword))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Username and password cannot be empty.", "OK");
                    return;
                }

                var success = await _authService.UpdatePasswordAsync(Username, NewPassword);

                if (success)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Password updated successfully!", "OK");
                    Username = string.Empty;
                    NewPassword = string.Empty;
                    await Shell.Current.GoToAsync("///LoginPage");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to update password. Please try again.", "OK");
                }

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error", "An Error Occured" + ex, "OK");
                NewPassword = string.Empty;
            }
        }   

    }
}
