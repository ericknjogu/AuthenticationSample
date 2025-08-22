using AuthenticationSample.Services;
using AuthenticationSample.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuthenticationSample.ViewModels
{
    public partial class HomePageViewModel: ObservableObject
    {
        [RelayCommand]

        public async void Logout()
        {
            var authservice = new AuthService();

            authservice.Logout();

            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
