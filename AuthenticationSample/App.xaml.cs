using AuthenticationSample.Services;
using AuthenticationSample.Views;

namespace AuthenticationSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            var authservice = new AuthService();

            var isLoggedIn = await authservice.IsLoggedInAsync();

            if (isLoggedIn)
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}